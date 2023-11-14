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
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PDFTransferDialog.xaml
    /// </summary>
    public partial class PDFTransferDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static string NEO_FILE_PATH = @"\\YPIIInterface2\ChannelData\Incoming\Neogenomics";
        private static string COPIER_MISC_FILE_PATH = @"\\fileserver\documents\scanning\misc";

        private Business.Test.AccessionOrder m_AccessionOrder;
        private List<string> m_Files;
        private List<string> m_CaseDocuments;
        private string m_MasterAccessionNo;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public PDFTransferDialog()
        {
            this.m_MasterAccessionNo = "19-";
            InitializeComponent();
            this.DataContext = this;
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        public List<string> Files
        {
            get { return this.m_Files; }
        }

        public List<string> CaseDocuments
        {
            get { return this.m_CaseDocuments; }
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set
            {
                if(this.m_AccessionOrder != value)
                {
                    this.m_AccessionOrder = value;
                    this.NotifyPropertyChanged("AccessionOrder");
                }
            }
        }        

        /*
        public string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
        */

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            this.m_AccessionOrder = null;
            this.m_CaseDocuments = null;

            if (this.ListViewFiles.SelectedItem != null)
            {
                string pdfFilePath = (string)this.ListViewFiles.SelectedItem;
                //string text = this.ExtractTextFromPdf(pdfFilePath);
                string[] lines = text.Split('\n');
                
                foreach (string line in lines)
                {                    
                    string regx = @"(Specimen ID: )(\d\d-\d+)";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regx);
                    System.Text.RegularExpressions.Match match = regex.Match(line);
                    if (match.Captures.Count !=0)
                    {
                        string masterAccessionNo = match.Groups[2].Value;
                        this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
                        
                        Business.OrderIdParser orderIdParser = new Business.OrderIdParser(masterAccessionNo);
                        string casePath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);
                        this.m_CaseDocuments = System.IO.Directory.GetFiles(casePath).ToList<string>();                        
                    }                    
                }                               
            }
            this.NotifyPropertyChanged("AccessionOrder");
            this.NotifyPropertyChanged("CaseDocuments");
            */
        }

        private void MenuItemOpenPDF_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewFiles.SelectedItem != null)
            {
                string pdfFilePath = (string)this.ListViewFiles.SelectedItem;
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo(pdfFilePath);
                p.StartInfo = info;
                p.Start();
            }
        }

        private void MenuItemLinkPDF_Click(object sender, RoutedEventArgs e)
        {            
            if(this.ListViewFiles.SelectedItem != null)
            {
                if (this.ListViewPanelSetOrders.SelectedItem != null)
                {
                    Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)this.ListViewPanelSetOrders.SelectedItem;
                    Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
                    Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

                    string sourcePDFFileName = (string)this.ListViewFiles.SelectedItem;
                    Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);
                    string casePath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);

                    if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
                    {                        
                        string pdfCaseFilePath = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);                        
                        System.IO.File.Copy(sourcePDFFileName, pdfCaseFilePath, true);

                        string xpsCaseFilePath = Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
                        this.GhostPDFToPNG(sourcePDFFileName, xpsCaseFilePath);

                        string tifCaseFilePath = Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
                        Business.Helper.FileConversionHelper.ConvertXPSToTIF(xpsCaseFilePath, tifCaseFilePath);
                    }
                    else
                    {
                        string neoCaseFileName = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser).Replace(".pdf", ".neoreport.pdf");
                        System.IO.File.Copy(sourcePDFFileName, neoCaseFileName, true);
                    }

                    this.m_CaseDocuments = System.IO.Directory.GetFiles(casePath).ToList<string>();
                    this.NotifyPropertyChanged(string.Empty);
                    MessageBox.Show("The pdf has been linked to this case.");
                }
                else
                {
                    MessageBox.Show("You must have a Test selected to perform this operation.");
                }
            }
            else
            {
                MessageBox.Show("You must have a file selected to perform this operation.");
            }            
        }        

        private void MenuItemViewResultPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrders.SelectedItem != null)
            {
                Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)this.ListViewPanelSetOrders.SelectedItem;
                Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
                if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                {
                    YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

                    YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
                    resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(ResultPathFactory_Finished);

                    this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                    bool started = resultPathFactory.Start(panelSetOrder, this.m_AccessionOrder, this.m_LoginPageWindow.PageNavigator, this.m_LoginPageWindow, System.Windows.Visibility.Collapsed);
                    if (started == true)
                    {
                        this.m_LoginPageWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("The result for this case is not available in this view.");
                    }
                }
                else
                {
                    MessageBox.Show("The result for this case is not available in this view.");
                }
            }
            else
            {
                MessageBox.Show("Select a case.");
            }
        }

        private void MenuItemCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrders.SelectedItem != null)
            {
                Business.Test.PanelSetOrder panelSetOrder = (Business.Test.PanelSetOrder)this.ListViewPanelSetOrders.SelectedItem;
                Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
                if(panelSet is YellowstonePathology.Business.PanelSet.Model.FISHTest)
                {
                    this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                    Billing.AddFISHCPTCodePage addCPTCodePage = new Billing.AddFISHCPTCodePage(panelSetOrder.ReportNo, this.m_AccessionOrder);
                    addCPTCodePage.Next += ResultPathFactory_Finished;
                    this.m_LoginPageWindow.PageNavigator.Navigate(addCPTCodePage);
                    this.m_LoginPageWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("The selected Test is not a FISH test.");
                }
            }
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        public void GhostPDFToPNG(string pdfResultFilePath, string xpsCaseFilePath)
        {
            string guid = System.Guid.NewGuid().ToString().ToUpper();
            
            string programDataPath = @"C:\ProgramData\ypi";
            string tmpFolderPath = System.IO.Path.Combine(programDataPath, guid);

            System.IO.Directory.CreateDirectory(tmpFolderPath);

            string gs = "c:\\program files\\gs\\gs9.25\\bin\\gswin64c.exe";
            string args = "-sDEVICE=png16m -dTextAlphabits=4 -r720x720 -sOutputFile=\"" + tmpFolderPath + "\\img_%00d.png\" \"" + pdfResultFilePath + "\" -DBATCH -dNOPAUSE -dNOPROMPT";

            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(gs, args);
            p.StartInfo = info;
            p.Start();
            p.WaitForExit();
            
            Business.Helper.FileConversionHelper.CreateXPSFromPNGFiles(tmpFolderPath, xpsCaseFilePath);            
        }

        private void MenuItemMoveToDone_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewFiles.SelectedItem != null)
            {
                string pdfFilePath = (string)this.ListViewFiles.SelectedItem;
                string fileName = System.IO.Path.GetFileName(pdfFilePath);
                string path = System.IO.Path.GetDirectoryName(pdfFilePath);
                string pdfFilePathDone = System.IO.Path.Combine(path, "Done", fileName);

                try
                {
                    System.IO.File.Move(pdfFilePath, pdfFilePathDone);
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                
                this.m_Files = System.IO.Directory.GetFiles(NEO_FILE_PATH, "*.pdf").ToList<string>();
                this.NotifyPropertyChanged("Files");
            }
        }

        private void HyperLinkOpenFolder_Click(object sender, RoutedEventArgs e)
        {                        
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", NEO_FILE_PATH);
            p.StartInfo = info;
            p.Start();
        }

        private void MenuItemRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_Files = System.IO.Directory.GetFiles(NEO_FILE_PATH, "*.pdf").ToList<string>();
            this.NotifyPropertyChanged("Files");
        }

        private void MenuItemViewAssignmentPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrders.SelectedItem != null)
            {
                UI.Login.FinalizeAccession.AssignmentPath assignmentPath = new UI.Login.FinalizeAccession.AssignmentPath(this.m_AccessionOrder);
                assignmentPath.Start();
            }
        }

        private void HyperLinkFindAccessionOrder_Click(object sender, RoutedEventArgs e)
        {            
            if(string.IsNullOrEmpty(this.m_MasterAccessionNo) == false)
            {
                this.m_AccessionOrder = null;
                this.m_CaseDocuments = null;

                this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(this.m_MasterAccessionNo, this);
                Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_MasterAccessionNo);
                string casePath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);
                this.m_CaseDocuments = System.IO.Directory.GetFiles(casePath).ToList<string>();

                this.NotifyPropertyChanged("AccessionOrder");
                this.NotifyPropertyChanged("CaseDocuments");
            }            
        }

        private void HyperLinkNeoResultFolder_Click(object sender, RoutedEventArgs e)
        {
            this.m_Files = System.IO.Directory.GetFiles(NEO_FILE_PATH, "*.pdf").ToList<string>();
            this.NotifyPropertyChanged("Files");
        }

        private void HyperLinkMiscFolder_Click(object sender, RoutedEventArgs e)
        {
            this.m_Files = System.IO.Directory.GetFiles(COPIER_MISC_FILE_PATH, "*.pdf").ToList<string>();
            this.NotifyPropertyChanged("Files");
        }
    }
}
