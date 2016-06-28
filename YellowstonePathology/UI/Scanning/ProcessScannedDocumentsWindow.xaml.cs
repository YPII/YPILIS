using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace YellowstonePathology.UI.Scanning
{
    /// <summary>
    /// Interaction logic for ProcessScannedDocumentsWindow.xaml
    /// </summary>
    public partial class ProcessScannedDocumentsWindow : Window, INotifyPropertyChanged
    {
        private delegate void MethodInvoker();
        public event PropertyChangedEventHandler PropertyChanged;

        //private string m_LocalStorageLocation = @"C:\Program Files\Yellowstone Pathology Institute\Scanning\LocalStorage";        
        //private string m_LocalSplitFileFolderPath = @"C:\Program Files\Yellowstone Pathology Institute\Scanning\LocalStorage\FilesToBeSplit\";                

        ScannedFileCollection m_ServerFileCollection;
        ServerFolderCollection m_ServerFolderCollection;

        string m_ReportNo;

        public ProcessScannedDocumentsWindow()
        {
            this.m_ReportNo = DateTime.Now.ToString("yy-");

            this.m_ServerFolderCollection = new Scanning.ServerFolderCollection();
            InitializeComponent();
            this.DataContext = this;
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                this.m_ReportNo = value;
                this.NotifyPropertyChanged("ReportNo");
            }
        }

        private void LoadImages(List<System.Drawing.Image> images)
        {
            foreach (System.Drawing.Image image in images)
            {
                System.Windows.Controls.Image controlsImage = new System.Windows.Controls.Image();

                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                memoryStream.Position = 0;

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.StreamSource = memoryStream;
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();

                controlsImage.Source = src;
                controlsImage.Stretch = Stretch.Uniform;

                this.StackPanelImage.Children.Add(controlsImage);
                memoryStream.Close();
            }
        }

        private List<System.Drawing.Image> GetAllPages(string file)
        {
            List<System.Drawing.Image> images = new List<System.Drawing.Image>();
            System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(file);
            int count = bitmap.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            for (int idx = 0; idx < count; idx++)
            {
                bitmap.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, idx);
                MemoryStream byteStream = new MemoryStream();
                bitmap.Save(byteStream, System.Drawing.Imaging.ImageFormat.Tiff);
                images.Add(System.Drawing.Image.FromStream(byteStream));
            }
            bitmap.Dispose();
            return images;
        }

        public ScannedFileCollection ServerFileCollection
        {
            get { return this.m_ServerFileCollection; }
        }

        public ServerFolderCollection ServerFolderCollection
        {
            get { return this.m_ServerFolderCollection; }
            set { this.m_ServerFolderCollection = value; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ComboBoxServerFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxServerFolder.SelectedItem != null)
            {
                ServerFolder serverFolder = (ServerFolder)this.ComboBoxServerFolder.SelectedItem;
                this.m_ServerFileCollection = new ScannedFileCollection();
                this.m_ServerFileCollection.LoadFiles(serverFolder.Path);
                this.NotifyPropertyChanged("ServerFileCollection");
            }
        }

        private void ListViewScannedFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewScannedFiles.SelectedItems.Count != 0)
            {
                ScannedFile scannedFile = (ScannedFile)this.ListViewScannedFiles.SelectedItem;
                this.StackPanelImage.Children.RemoveRange(0, this.StackPanelImage.Children.Count);
                List<System.Drawing.Image> images = this.GetAllPages(scannedFile.Name);
                this.LoadImages(images);
                this.SetFocus();
            }
        }

        private void ButtonMove_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxServerFolder.SelectedItem != null)
            {
                ServerFolder serverFolder = (ServerFolder)this.ComboBoxServerFolder.SelectedItem;
                if (this.ListViewScannedFiles.SelectedItems.Count != 0)
                {
                    ScannedFile scannedFile = (ScannedFile)this.ListViewScannedFiles.SelectedItem;

                    YellowstonePathology.Business.Document.CaseDocumentCollection caseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_ReportNo);
                    YellowstonePathology.Business.Document.CaseDocumentCollection requisitions = caseDocumentCollection.GetRequisitions();

                    int nextReqNo = requisitions.Count + 1;
                    YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ReportNo);
                    if (orderIdParser.ReportNo != null || orderIdParser.MasterAccessionNo != null)
                    {
                        string newFileName = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), this.m_ReportNo + ".REQ." + nextReqNo.ToString() + ".TIF");
                        if (orderIdParser.IsLegacyReportNo == false)
                        {
                            string masterAccessionNo = orderIdParser.MasterAccessionNo;
                            newFileName = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), masterAccessionNo + ".REQ." + nextReqNo.ToString() + ".TIF");
                        }

                        if (scannedFile.Extension.ToUpper() == ".TIF")
                        {
                            System.IO.File.Copy(scannedFile.Name, newFileName);
                        }
                        else if (scannedFile.Extension.ToUpper() == ".JPG")
                        {
                            System.Drawing.Imaging.ImageCodecInfo myImageCodecInfo;
                            System.Drawing.Imaging.Encoder myEncoder;
                            System.Drawing.Imaging.EncoderParameter myEncoderParameter;
                            System.Drawing.Imaging.EncoderParameters myEncoderParameters;

                            myImageCodecInfo = GetEncoderInfo("image/tiff");
                            myEncoder = System.Drawing.Imaging.Encoder.Compression;
                            myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);

                            myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, (long)System.Drawing.Imaging.EncoderValue.CompressionCCITT4);
                            myEncoderParameters.Param[0] = myEncoderParameter;

                            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(scannedFile.Name);
                            bitmap.Save(newFileName, myImageCodecInfo, myEncoderParameters);
                            bitmap.Dispose();
                        }

                        System.IO.File.Delete(scannedFile.Name);

                        this.StackPanelImage.Children.RemoveRange(0, this.StackPanelImage.Children.Count);
                        this.m_ServerFileCollection = new ScannedFileCollection();
                        this.m_ServerFileCollection.LoadFiles(serverFolder.Path);
                        this.NotifyPropertyChanged("ServerFileCollection");
                        this.ListViewScannedFiles.SelectedIndex = 0;

                        if (orderIdParser.IsLegacyReportNo) this.ReportNo = this.ReportNo.Substring(0, 4);
                        else this.ReportNo = this.ReportNo.Substring(0, 3);
                    }
                    else
                    {
                        MessageBox.Show("The Master Accession No should be used for the new style report - 13-123.S \nThe Report No needs to be used for old style reports - S13-123", "Use correct identifier");
                    }
                }
            }
        }

        private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            System.Drawing.Imaging.ImageCodecInfo[] encoders;
            encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void SetFocus()
        {
            ThreadPool.QueueUserWorkItem(delegate (object o)
            {
                TextBox element = (TextBox)o;
                element.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    (MethodInvoker)delegate ()
                    {
                        element.Focus();
                        Keyboard.Focus(element);
                        element.SelectionStart = element.Text.Length;
                    });
            }, this.TextBoxReportNo);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxServerFolder.SelectedItem != null)
            {
                ServerFolder serverFolder = (ServerFolder)this.ComboBoxServerFolder.SelectedItem;
                if (this.ListViewScannedFiles.SelectedItems.Count != 0)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Delete this document?", "Delete", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        ScannedFile scannedFile = (ScannedFile)this.ListViewScannedFiles.SelectedItem;
                        System.IO.File.Delete(scannedFile.Name);

                        this.StackPanelImage.Children.RemoveRange(0, this.StackPanelImage.Children.Count);
                        this.m_ServerFileCollection = new ScannedFileCollection();
                        this.m_ServerFileCollection.LoadFiles(serverFolder.Path);
                        this.NotifyPropertyChanged("ServerFileCollection");
                        this.ListViewScannedFiles.SelectedIndex = 0;
                        this.ReportNo = "13-";
                    }
                }
            }
        }
    }
}
