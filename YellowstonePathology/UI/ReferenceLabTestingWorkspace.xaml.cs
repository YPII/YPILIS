using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ReferenceLabTestingWorkspace.xaml
    /// </summary>
    public partial class ReferenceLabTestingWorkspace : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private List<string> m_DocumentTypes;
        private string m_SelectedDocumentType;
        private string m_SelectedEmail;

        private List<System.IO.FileInfo> m_FileInfoList;
        private List<System.IO.DirectoryInfo> m_DirectoryInfoList;
        private System.IO.DirectoryInfo m_SelectedMoveToDirectoryInfo;

        private string m_MasterAccessionNo;
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Hyperlink m_SelectedHyperlink;

        private const string NEO_FOLDER_PATH = @"\\ypiiinterface2\ChannelData\Incoming\Neogenomics";
        private const string FAX_FOLDER_PATH = @"\\fileserver\Documents\Faxes";        

        private KeyValuePair<string, string> m_SelectedFolderPath;
        private bool m_ShowArchiveFolder;
        private List<ContextMenu> m_ContextMenuList;

        public ReferenceLabTestingWorkspace()
        {
            this.m_ContextMenuList = new List<ContextMenu>();

            this.m_DocumentTypes = new List<string>();
            this.m_DocumentTypes.Add("Case Report");
            this.m_DocumentTypes.Add("Requisition");
            this.m_SelectedDocumentType = "Case Report";

            this.m_MasterAccessionNo = $"{DateTime.Today.ToString("yy")}-";
            this.m_ShowArchiveFolder = false;
            
            this.BuildDirectoryInfoList();

            InitializeComponent();            
            this.DataContext = this;
        }

        public void CreateMoveToContextMenu(System.IO.DirectoryInfo selectedDirectoryInfo)
        {
            //Michelle Nelson, Kevin Benge, Lisha Lutke, & Dona Cranston

            this.ContextMenuMoveTo.Items.Clear();
            this.ContextMenuMoveTo.IsEnabled = false;
            foreach(System.IO.DirectoryInfo di in this.m_DirectoryInfoList)
            {
                if(selectedDirectoryInfo.FullName != di.FullName)
                {
                    MenuItem cm = new MenuItem();                    
                    cm.Header = $"{selectedDirectoryInfo.Name}->{di.Name}";
                    cm.Tag = di;
                    cm.Click += ContextMenuMoveTo_Click;
                    this.ContextMenuMoveTo.Items.Add(cm);                    
                }                
            }

            List<string> emailList = new List<string>();
            emailList.Add("dona.cranson@ypii.com");
            emailList.Add("lisha.lutke@ypii.com");
            emailList.Add("kevin.benge@ypii.com");            
            emailList.Add("michelle.nelson@ypii.com");
            emailList.Add("sid.harder@ypii.com");
            emailList.Add("eric.ramsey@ypii.com");

            foreach (string email in emailList)
            {
                MenuItem cm = new MenuItem();
                cm.Header = $"email->{email}";
                cm.Tag = email;
                cm.Click += ContextMenuEmailTo_Click;
                this.ContextMenuMoveTo.Items.Add(cm);
            }
        }

        private void ContextMenuMoveTo_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            this.m_SelectedMoveToDirectoryInfo = (System.IO.DirectoryInfo)menuItem.Tag;            

            this.WebBrowser.Navigate("about:blank");
            this.WebBrowser.LoadCompleted += WebBrowser_LoadCompleted_ForMove;
        }

        private void ContextMenuEmailTo_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            this.m_SelectedEmail = (string)menuItem.Tag;
            this.WebBrowser.Navigate("about:blank");
            this.WebBrowser.LoadCompleted += WebBrowser_LoadCompleted_ForEmail;
        }

        private void WebBrowser_LoadCompleted_ForEmail(object sender, NavigationEventArgs e)
        {
            System.IO.FileInfo sourceFileInfo = (System.IO.FileInfo)this.ListViewDocuments.SelectedItem;
            string destinationFileName = System.IO.Path.Combine(@"\\fileserver\Documents\Faxes\Done\", sourceFileInfo.Name);

            string msg = "Attached is a fax that came in for you.";
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", this.m_SelectedEmail, System.Windows.Forms.SystemInformation.UserName, msg);
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.110");
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(sourceFileInfo.FullName);
            message.Attachments.Add(attachment);

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);
            
            int i = 0;
            while (i < 200)
            {
                try
                {
                    System.IO.File.Move(sourceFileInfo.FullName, destinationFileName);
                    break;
                }
                catch
                {
                    i += 1;
                    GC.Collect();
                }
            }

            if (i == 200) MessageBox.Show("The file could not be moved at this time because it is being used by another process.");

            System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)this.ListViewDirectories.SelectedItem;
            this.BuildFileInfoList(di);
            this.NotifyPropertyChanged(string.Empty);            

            this.WebBrowser.LoadCompleted -= WebBrowser_LoadCompleted_ForEmail;
        }

        private void WebBrowser_LoadCompleted_ForMove(object sender, NavigationEventArgs e)
        {
            System.IO.FileInfo sourceFileInfo = (System.IO.FileInfo)this.ListViewDocuments.SelectedItem;
            string destinationFileName = System.IO.Path.Combine(this.m_SelectedMoveToDirectoryInfo.FullName, sourceFileInfo.Name);

            int i = 0;
            while (i < 100)
            {
                try
                {
                    System.IO.File.Move(sourceFileInfo.FullName, destinationFileName);
                    break;
                }
                catch
                {
                    i += 1;
                    GC.Collect();
                }
            }

            if (i == 100) MessageBox.Show("The file could not be moved at this time because it is being used by another process.");

            System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)this.ListViewDirectories.SelectedItem;
            this.BuildFileInfoList(di);            
            this.NotifyPropertyChanged(string.Empty);

            this.WebBrowser.LoadCompleted -= WebBrowser_LoadCompleted_ForMove;
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public KeyValuePair<string, string> SelectedFolderPath
        {
            get { return this.m_SelectedFolderPath; }
            set { this.m_SelectedFolderPath = value; }
        }        

        public bool ShowArchiveFolder
        {
            get { return this.m_ShowArchiveFolder; }
            set { this.m_ShowArchiveFolder = value; }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        public List<string> DocumentTypes
        {
            get { return this.m_DocumentTypes; }
        }

        public string SelectedDocumentType
        {
            get { return this.m_SelectedDocumentType; }
            set { this.m_SelectedDocumentType = value; }
        }

        private void BuildDirectoryInfoList()
        {
            this.m_DirectoryInfoList = new List<System.IO.DirectoryInfo>();
            this.m_DirectoryInfoList.Add(new System.IO.DirectoryInfo(NEO_FOLDER_PATH));
            this.m_DirectoryInfoList.Add(new System.IO.DirectoryInfo(FAX_FOLDER_PATH));

            IEnumerable<string> faxFolders = System.IO.Directory.EnumerateDirectories(FAX_FOLDER_PATH);
            foreach(string faxFolder in faxFolders)
            {
                this.m_DirectoryInfoList.Add(new System.IO.DirectoryInfo(faxFolder));
            }            
        }

        private void BuildFileInfoList(System.IO.DirectoryInfo directoryInfo)
        {
            this.m_FileInfoList = new List<System.IO.FileInfo>();

            System.IO.FileInfo [] files = directoryInfo.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                if(file.Extension.ToUpper().EndsWith(".PDF") == true)
                {
                    this.m_FileInfoList.Add(new System.IO.FileInfo(file.FullName));
                }                
            }
                
            this.m_FileInfoList.Sort((x, y) => DateTime.Compare(x.CreationTime, y.CreationTime));
            this.m_FileInfoList.Reverse();
        }

        public List<System.IO.FileInfo> FileInfoList
        {
            get { return this.m_FileInfoList; }
        }

        public List<System.IO.DirectoryInfo> DirectoryInfoList
        {
            get { return this.m_DirectoryInfoList; }
        }

        private void ListViewDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewDocuments.SelectedItem != null)
            {
                System.IO.FileInfo document = (System.IO.FileInfo)this.ListViewDocuments.SelectedItem;
                string url = $"file:{document.FullName.Replace('\\', '/')}";
                this.ContextMenuMoveTo.IsEnabled = false;
                this.WebBrowser.Navigate(url);
                this.WebBrowser.LoadCompleted += WebBrowser_LoadCompleted_ForEnablePublish;
            }
        }
        
        private void WebBrowser_LoadCompleted_ForEnablePublish(object sender, NavigationEventArgs e)
        {
            this.WebBrowser.LoadCompleted -= WebBrowser_LoadCompleted_ForEnablePublish;
            this.ContextMenuMoveTo.IsEnabled = true;
        }

        private void TextBoxMasterAccessionNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(this.m_MasterAccessionNo, this);
                this.NotifyPropertyChanged(string.Empty);

                var eventArgs = new TextCompositionEventArgs(Keyboard.PrimaryDevice, new TextComposition(InputManager.Current, Keyboard.FocusedElement, ""));
                eventArgs.RoutedEvent = TextInputEvent;

                InputManager.Current.ProcessInput(eventArgs);
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewPanelSetOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ContextMenuSavePDF_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperLinkAcceptCase_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)sender;
            Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)hyperLink.Tag;
            this.ListViewPanelSetOrders.SelectedItem = panelSetOrder;

            if (panelSetOrder.Accepted == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"This test has already been accepted, would you like to unaccept it?", "Already Accepted?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    panelSetOrder.Unaccept();
                    Business.Persistence.DocumentGateway.Instance.Save();
                }
            }
            else
            {
                panelSetOrder.Accept();
                Business.Persistence.DocumentGateway.Instance.Save();
            }
        }
       
        private void HyperLinkPublishPDF_Click(object sender, RoutedEventArgs e)
        {            
            Hyperlink hyperLink = (Hyperlink)sender;
            this.m_SelectedHyperlink = hyperLink;

            Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)hyperLink.Tag;
            this.ListViewPanelSetOrders.SelectedItem = panelSetOrder;
            if (this.ListViewDocuments.SelectedItem != null)
            {
                Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);
                string pdfFilePath = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);
                if (this.m_SelectedDocumentType == "Case Report" && System.IO.File.Exists(pdfFilePath) == true)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show($"The file {pdfFilePath} already exists, would you like to replace it?", "Replace File?", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                this.WebBrowser.Navigate("about:blank");
                this.WebBrowser.LoadCompleted += WebBrowser_LoadCompleted_ForPublish;
            }
            else
            {
                MessageBox.Show("Please select a document to publish.");
            }            
        }

        private int GetNextReqNumber(System.IO.DirectoryInfo directoryInfo)
        {            
            System.IO.FileInfo[] fileInfos = directoryInfo.GetFiles();
            List<System.IO.FileInfo> reqFileInfos = new List<System.IO.FileInfo>();

            foreach (System.IO.FileInfo fileInfo in fileInfos)
            {
                if(fileInfo.Name.Contains("REQ") == true)
                {
                    reqFileInfos.Add(fileInfo);
                }
            }

            int largestNo = 0;
            foreach (System.IO.FileInfo fileInfo in reqFileInfos)
            {
                int no = Convert.ToInt32(fileInfo.Name.Split('.')[2]);
                if (no > largestNo) largestNo = no;
            }

            return largestNo + 1;
        }

        private void WebBrowser_LoadCompleted_ForPublish(object sender, NavigationEventArgs e)
        {
            Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)this.m_SelectedHyperlink.Tag;
            this.ListViewPanelSetOrders.SelectedItem = panelSetOrder;

            string pdfFilePath = String.Empty;
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);
            if (this.m_SelectedDocumentType == "Case Report")
            {                
                pdfFilePath = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);
            }
            else
            {
                System.IO.DirectoryInfo caseDocumentDi = new System.IO.DirectoryInfo(Business.Document.CaseDocumentPath.GetPath(orderIdParser));
                int nextReqNumber = this.GetNextReqNumber(caseDocumentDi);
                pdfFilePath =  caseDocumentDi.FullName + orderIdParser.MasterAccessionNo + ".REQ." + nextReqNumber.ToString() + ".PDF";
            }
            
            System.IO.FileInfo fileToMove = (System.IO.FileInfo)this.ListViewDocuments.SelectedItem;                        
            System.IO.File.Copy(fileToMove.FullName, pdfFilePath, true);

            string doneFilePath = $@"{fileToMove.DirectoryName}\done\{MongoDB.Bson.ObjectId.GenerateNewId().ToString()}.pdf";            

            int i = 0;
            while (i < 100)
            {
                try
                {
                    System.IO.File.Move(fileToMove.FullName, doneFilePath);
                    break;
                }
                catch
                {
                    i += 1;
                    GC.Collect();
                }
            }

            if (i == 100)
            {
                MessageBox.Show("The file could not be moved at this time because it is being used by another process.");
            }
            else
            {
                MessageBox.Show("The PDF file has been published.");
            }            

            System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)this.ListViewDirectories.SelectedItem;
            this.BuildFileInfoList(di);

            this.NotifyPropertyChanged(string.Empty);

            this.WebBrowser.LoadCompleted -= WebBrowser_LoadCompleted_ForPublish;
        }        

        private void HyperLinkOpenCaseDocumentFolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_AccessionOrder != null && this.m_AccessionOrder.PanelSetOrderCollection.Count != 0)
            {
                YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo);
                string folderPath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
                p.StartInfo = info;
                p.Start();
            }
        }        

        private void ComboboxDocumentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewDirectories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewDirectories.SelectedItem != null)
            {
                System.IO.DirectoryInfo di = (System.IO.DirectoryInfo)this.ListViewDirectories.SelectedItem;
                this.BuildFileInfoList(di);
                this.CreateMoveToContextMenu(di);
                this.NotifyPropertyChanged(string.Empty);
            }
        }       
    }
}
