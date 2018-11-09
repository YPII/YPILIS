﻿using System;
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
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PDFTransferDialog.xaml
    /// </summary>
    public partial class PDFTransferDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static string NEO_FILE_PATH = @"\\ypiiinterface1\ChannelData\Incoming\Neogenomics";

        private Business.Test.AccessionOrder m_AccessionOrder;
        private List<string> m_Files;
        private List<string> m_CaseDocuments;

        public PDFTransferDialog()
        {            
            this.m_Files = System.IO.Directory.GetFiles(NEO_FILE_PATH, "*.pdf").ToList<string>();
            InitializeComponent();
            this.DataContext = this;
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewFiles.SelectedItem != null)
            {
                string pdfFilePath = (string)this.ListViewFiles.SelectedItem;
                string text = this.ExtractTextFromPdf(pdfFilePath);
                string[] lines = text.Split('\n');
                
                foreach (string line in lines)
                {                    
                    string regx = @"(Specimen ID: )(\d\d-\d+)";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regx);
                    System.Text.RegularExpressions.Match match = regex.Match(line);
                    if (match.Captures.Count !=0)
                    {
                        string masterAccessionNo = match.Groups[2].Value;
                        this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                        
                        Business.OrderIdParser orderIdParser = new Business.OrderIdParser(masterAccessionNo);
                        string casePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                        this.m_CaseDocuments = System.IO.Directory.GetFiles(casePath).ToList<string>();

                        this.NotifyPropertyChanged("AccessionOrder");
                        this.NotifyPropertyChanged("CaseDocuments");
                    }                    
                }                               
            }
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
                    string casePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);

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

        private void MenuItemTesting_Click(object sender, RoutedEventArgs e)
        {
            
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
    }
}
