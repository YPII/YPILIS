﻿using System;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Billing
{    
    public partial class EODProcessingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        private System.ComponentModel.BackgroundWorker m_BackgroundWorker;
        private Nullable<DateTime> m_PostDate;

        private string m_BaseWorkingFolderPathPSA = @"\\CFileServer\Documents\Billing\PSA\";
        private string m_BaseWorkingFolderPathSVH = @"\\CFileServer\Documents\Billing\SVH\";

        private ObservableCollection<string> m_StatusMessageList;
        private string m_StatusCountMessage;
        private string m_CurrentProcess;
        private int m_StatusCount;
        private List<string> m_ReportNumbersToProcess;

        public EODProcessingDialog()
        {
            this.m_ReportNumbersToProcess = new List<string>();
            this.m_StatusCount = 0;
            this.m_StatusMessageList = new ObservableCollection<string>();
            this.m_StatusMessageList.Add("No Status");

            this.m_PostDate = DateTime.Today;                                                
            InitializeComponent();
            this.DataContext = this;

            Closing += PSATransferDialog_Closing;
        }

        private void PSATransferDialog_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public string StatusCountMessage
        {
            get { return this.m_StatusCountMessage; }
        }

        public string CurrentProcess
        {
            get { return this.m_CurrentProcess; }
        }

        public ObservableCollection<string> StatusMessageList
        {
            get { return this.m_StatusMessageList; }
        }

        public Nullable<DateTime> PostDate
        {
            get { return this.m_PostDate; }
            set { this.m_PostDate = value; }
        }

        private void MenuItemOpenPSAFolder_Click(object sender, RoutedEventArgs e)
        {
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.Value.ToString("MMddyyyy"));
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", workingFolder);
            p.StartInfo = info;
            p.Start();
        }

        private void MenuItemOpenSVHFolder_Click(object sender, RoutedEventArgs e)
        {
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.Value.ToString("MMddyyyy"));
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", workingFolder);
            p.StartInfo = info;
            p.Start();
        }

        private void MenuItemSendClinicCaseEmail_Click(object sender, RoutedEventArgs e)
        {
            Business.Billing.Model.SVHClinicMailMessage.SendMessage(DateTime.Today.Subtract(TimeSpan.FromDays(1)));
            MessageBox.Show("An email was sent for yesterday's SVH clinic cases.");
        }

        private void MenuItemProcessPSAFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_CurrentProcess = "Processing PSA Files.";
            this.NotifyPropertyChanged("CurrentProcess");

            if (this.m_PostDate.HasValue == true)
            {
                this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
                this.m_BackgroundWorker.WorkerSupportsCancellation = false;
                this.m_BackgroundWorker.WorkerReportsProgress = true;
                this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
                this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(TransferFilesToFolder);
                this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
                this.m_BackgroundWorker.RunWorkerAsync();
            }
        }

        private void MenuItemProcessSVHFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_CurrentProcess = "Processing SVH Files.";
            this.NotifyPropertyChanged("CurrentProcess");

            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(CreateSVHCDMFiles);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void MenuItemTransferPSAFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(TransferPSAFiles);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void MenuItemTransferSVHFiles_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                this.m_StatusCount += 1;
                string message = (string)e.UserState;
                this.m_StatusMessageList.Insert(0, message);
                this.m_StatusCountMessage = this.m_StatusCount.ToString();
                this.NotifyPropertyChanged("StatusCountMessage");                
            }));                        
        }        

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Processing has completed.");
        }

        private string GetUnconfirmedReportNumbers(List<string> reportNumbersProcessed)
        {
            StringBuilder result = new StringBuilder();
            foreach (string reportNumber in this.m_ReportNumbersToProcess)
            {                
                if (reportNumbersProcessed.Exists(x => x == reportNumber) == false)
                {
                    result.Append(reportNumber + " ");
                }
            }
            return result.ToString();
        }

        private void CheckPatientTifFiles()
        {
			YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate.Value);
            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo.Value);
                string patientTifFilePath = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePatientTif(orderIdParser);
                string patientTifFileName = System.IO.Path.GetFileName(patientTifFilePath);
                string workingFolderPath = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.Value.ToString("MMddyyyy"));
                if (System.IO.File.Exists(patientTifFilePath) == true)
                {
                    string psaFileName = workingFolderPath + @"\" + orderIdParser.MasterAccessionNo + ".Patient.tif";
                    if(System.IO.File.Exists(psaFileName) == false)
                    {
                        MessageBox.Show(psaFileName);
                    }
                }
            }
        }

        private void TransferFilesToFolder(object sender, System.ComponentModel.DoWorkEventArgs e)
        {                                    
			YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate.Value);
            string workingFolder = System.IO.Path.Combine(m_BaseWorkingFolderPathPSA, this.m_PostDate.Value.ToString("MMddyyyy"));
            this.SetupWorkingFolders(workingFolder);

            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
                string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);

                if (accessionOrder.UseBillingAgent == true)
                {
                    if (panelSetOrder.IsBillable == true)
                    {
                        if (panelSetOrder.PanelSetOrderCPTCodeBillCollection.HasItemsToSendToPSA() == true)
                        {
                            this.m_ReportNumbersToProcess.Add(reportNo.Value);
                            this.CreatePatientTifFile(reportNo.Value);
                            this.CreateXmlBillingDocument(accessionOrder, reportNo.Value);
                            this.CopyFiles(reportNo.Value, accessionOrder.MasterAccessionNo, workingFolder);

                            this.m_BackgroundWorker.ReportProgress(1, reportNo.Value + " Complete.");
                        }
                    }
                }
            }                        
        }

        private void CreateSVHCDMFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate.Value);
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.Value.ToString("MMddyyyy"));
            this.SetupWorkingFolders(workingFolder);

            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
                string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                if(accessionOrder.SvhMedicalRecord.Substring(0) == "A")
                {
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);
                    foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in panelSetOrder.PanelSetOrderCPTCodeBillCollection)
                    {
                        if (panelSetOrderCPTCodeBill.BillTo == "Client")
                        {
                            List<Business.Test.PanelSetOrderCPTCodeBill> panelSetOrderCPTCodeBillList = new List<Business.Test.PanelSetOrderCPTCodeBill>();
                            panelSetOrderCPTCodeBillList.Add(panelSetOrderCPTCodeBill);
                            Business.HL7View.EPIC.EPICFT1ResultView epicFT1ResultView = new Business.HL7View.EPIC.EPICFT1ResultView(accessionOrder, panelSetOrderCPTCodeBillList, true);                            
                            epicFT1ResultView.Save(this.m_BaseWorkingFolderPathSVH);                            
                        }
                    }
                }                
            }
        }

        private void SetupWorkingFolders(string workingFolder)
        {            
            if (Directory.Exists(workingFolder) == false)
            {
                Directory.CreateDirectory(workingFolder);
            }
        }

        private void CreateXmlBillingDocument(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            YellowstonePathology.Business.Billing.Model.PSABillingDocument psaBillingDocument = new YellowstonePathology.Business.Billing.Model.PSABillingDocument(accessionOrder, reportNo);
            psaBillingDocument.Build();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string filePath = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), reportNo + ".BillingDetails.xml");
            psaBillingDocument.Save(filePath);
        }

        private void CopyFiles(string reportNo, string masterAccessionNo, string workingFolder)
        {
            YellowstonePathology.Business.Document.CaseDocumentCollection caseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(reportNo);
            YellowstonePathology.Business.Document.CaseDocumentCollection billingCaseDocumentCollection = caseDocumentCollection.GetPsaFiles(reportNo, masterAccessionNo);

            foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in billingCaseDocumentCollection)
            {                
                string sourceFile = caseDocument.FullFileName;
                string destinationFile = System.IO.Path.Combine(workingFolder, System.IO.Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile, true);
            }
        }          

        private void CreatePatientTifFile(string reportNo)
        {
            List<YellowstonePathology.Business.Patient.Model.SVHBillingData> sVHBillingDataList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientImportDataList(reportNo);
			if (sVHBillingDataList.Count != 0)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
                {                    
					YellowstonePathology.Business.Document.SVHBillingDocument svhBillingDocument = new Business.Document.SVHBillingDocument(sVHBillingDataList[0]);
                    svhBillingDocument.SaveAsTIF(orderIdParser);                    
                }
                ));                    
            }            
        }        

        private void RenameReqFiles(string reportNo, string reportNoLetter, int startFileNumber)
        {
            int fileCount = startFileNumber;
            string wrongName = "." + reportNoLetter + ".REQ." + fileCount.ToString() + ".TIF";
            string correctName = ".REQ." + fileCount.ToString() + ".TIF";
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			string wrongFile = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), orderIdParser.MasterAccessionNo + wrongName);
			string correctFile = System.IO.Path.Combine(YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser), orderIdParser.MasterAccessionNo + correctName);

            if (System.IO.File.Exists(wrongFile) == true)
            {
                if (System.IO.File.Exists(correctFile) == false)
                {
                    System.IO.File.Move(wrongFile, correctFile);
                }
                else
                {
                    fileCount += 1;
                    this.RenameReqFiles(reportNo, reportNoLetter, fileCount);
                }
            }
        }

        public List<string> GetReportNoListFromFolder()
        {
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.Value.ToString("MMddyyyy"));
            List<string> result = new List<string>();
            string[] files = System.IO.Directory.GetFiles(workingFolder);
            foreach (string file in files)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                string[] splitFile = fileName.Split('.');

                if (splitFile.Length == 3)
                {
                    if (splitFile[2] == "BillingDetails")
                    {
                        string reportNo = splitFile[0] + '.' + splitFile[1];
                        result.Add(reportNo);
                    }
                }
            }
            return result;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }              

        private void TransferPSAFiles(object sender, System.ComponentModel.DoWorkEventArgs e)        
        {
            string configFilePath = @"C:\Program Files\Yellowstone Pathology Institute\psa-ssh-config.json";

            if (System.IO.File.Exists(configFilePath) == true)
            {
                JObject psaSSHConfig = null;
                using (StreamReader file = File.OpenText(configFilePath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    psaSSHConfig = (JObject)JToken.ReadFrom(reader);
                }

                string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.Value.ToString("MMddyyyy"));
                if(System.IO.Directory.Exists(workingFolder) == true)
                {
                    string[] files = System.IO.Directory.GetFiles(workingFolder);

                    Business.SSHFileTransfer sshFileTransfer = new Business.SSHFileTransfer(psaSSHConfig["host"].ToString(), Convert.ToInt32(psaSSHConfig["port"]),
                        psaSSHConfig["username"].ToString(), psaSSHConfig["password"].ToString());

                    sshFileTransfer.StatusMessage += SSHFileTransfer_StatusMessage;
                    sshFileTransfer.UploadFilesToPSA(files);
                }
                else
                {
                    MessageBox.Show("The folder for this date does not exist.");
                }
                
            }
            else
            {
                MessageBox.Show("The PSA Config file could not be found.");
            }
        }

        private void SSHFileTransfer_StatusMessage(object sender, string message, int count)
        {
            this.m_StatusCount = count;
            this.m_BackgroundWorker.ReportProgress(1, message);
        }        
    }
}