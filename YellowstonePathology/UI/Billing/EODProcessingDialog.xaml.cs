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
        private DateTime m_PostDate;
        
        private string m_BaseWorkingFolderPathPSA = @"\\CFileServer\Documents\Billing\PSA\";
        private string m_BaseWorkingFolderPathSVH = @"\\CFileServer\Documents\Billing\SVH\";

        private ObservableCollection<string> m_StatusMessageList;
        private string m_StatusCountMessage;        
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

        public ObservableCollection<string> StatusMessageList
        {
            get { return this.m_StatusMessageList; }
        }

        public DateTime PostDate
        {
            get { return this.m_PostDate; }
            set { this.m_PostDate = value; }
        }

        private void MenuItemOpenPSAFolder_Click(object sender, RoutedEventArgs e)
        {
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.ToString("MMddyyyy"));
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", workingFolder);
            p.StartInfo = info;
            p.Start();
        }

        private void MenuItemOpenSVHFolder_Click(object sender, RoutedEventArgs e)
        {
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.ToString("MMddyyyy"));
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", workingFolder);
            p.StartInfo = info;
            p.Start();
        }

        private void MenuItemSendClinicCaseEmail_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(SendSVHClinicEmail);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();            
        }

        private void SendSVHClinicEmail(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Sending SVH Clinic Email: " + DateTime.Now.ToLongTimeString());
            int rowCount = Business.Billing.Model.SVHClinicMailMessage.SendMessage();
            this.m_BackgroundWorker.ReportProgress(1, "SVH Clinic Email Sent with " + rowCount.ToString() + " rows");
        }

        private void MenuItemProcessPSAFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();                                    
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(ProcessPSAFiles);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();        
        }

        private void MenuItemProcessSVHFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();            
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(ProcessSVHCDMFiles);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void MenuItemTransferPSAFiles_Click(object sender, RoutedEventArgs e)
        {
            this.TransferPSAFiles();
        }

        private void TransferPSAFiles()
        {
            this.m_StatusMessageList.Clear();
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
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(TransferSVHFiles);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void TransferSVHFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting Transfer of SVH Files: " + DateTime.Now.ToLongTimeString());
            string destinationFolder = @"\\ypiiinterface1\ChannelData\Outgoing\1002";

            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.ToString("MMddyyyy"), "ft1");
            string[] files = System.IO.Directory.GetFiles(workingFolder);
            
            foreach(string file in files)
            {
                this.m_BackgroundWorker.ReportProgress(1, "Copying File: " + file);
                string destinationFile = System.IO.Path.Combine(destinationFolder, System.IO.Path.GetFileName(file));
                System.IO.File.Copy(file, destinationFile);                
                rowCount += 1;
            }

            foreach (string file in files)
            {
                this.m_BackgroundWorker.ReportProgress(1, "Moving File: " + file);
                string destinationFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(file), "done", System.IO.Path.GetFileName(file));
                System.IO.File.Move(file, destinationFile);
                rowCount += 1;
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished Transfer of " + rowCount + " SVH Files");
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
			YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate);
            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo.Value);
                string patientTifFilePath = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePatientTif(orderIdParser);
                string patientTifFileName = System.IO.Path.GetFileName(patientTifFilePath);
                string workingFolderPath = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.ToString("MMddyyyy"));
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

        private void ProcessPSAFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing PSA Files: " + DateTime.Now.ToLongTimeString());

            YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate);
            string workingFolder = System.IO.Path.Combine(m_BaseWorkingFolderPathPSA, this.m_PostDate.ToString("MMddyyyy"));            
            if (Directory.Exists(workingFolder) == false)
                Directory.CreateDirectory(workingFolder);

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
                            rowCount += 1;
                        }
                    }
                }
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished processing " + rowCount + " PSA Files");
        }

        private void ProcessSVHCDMFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing SVH CDM files.");
            YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbersBySVHProcess(this.m_PostDate);
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.ToString("MMddyyyy"));
            if (Directory.Exists(workingFolder) == false)
            {
                Directory.CreateDirectory(workingFolder);
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "ft1"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "ft1", "done"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "result"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "result", "done"));
            }                

            Business.Billing.Model.CptCodeCollection cptCodeCollection = new Business.Billing.Model.CptCodeCollection();
            cptCodeCollection.Load();

            int rowCount = 0;
            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {                
                this.m_BackgroundWorker.ReportProgress(1, "Processing: " + reportNo.Value);

                string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);               
                
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);                
                foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in panelSetOrder.PanelSetOrderCPTCodeBillCollection)
                {
                    if (panelSetOrderCPTCodeBill.BillTo == "Client" && panelSetOrderCPTCodeBill.PostDate == this.m_PostDate)
                    {                                                
                        if(cptCodeCollection.HasSVHCDM(panelSetOrderCPTCodeBill.CPTCode) == true)
                        {                            
                            if(panelSetOrderCPTCodeBill.PostedToClient == false)
                            {
                                if(string.IsNullOrEmpty(panelSetOrderCPTCodeBill.MedicalRecord) == false && string.IsNullOrEmpty(panelSetOrderCPTCodeBill.Account) == false)
                                {
                                    if (panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("V") == true)
                                    {
                                        this.m_BackgroundWorker.ReportProgress(1, "Writing File: " + reportNo.Value + " - " + panelSetOrderCPTCodeBill.CPTCode);
                                        Business.HL7View.EPIC.EPICFT1ResultView epicFT1ResultView = new Business.HL7View.EPIC.EPICFT1ResultView(accessionOrder, panelSetOrderCPTCodeBill);
                                        epicFT1ResultView.Publish(System.IO.Path.Combine(workingFolder, "ft1"));
                                        panelSetOrderCPTCodeBill.PostedToClient = true;
                                        panelSetOrderCPTCodeBill.PostedToClientDate = DateTime.Now;
                                        rowCount += 1;
                                    }
                                    else
                                    {
                                        throw new Exception("The MRN for this charge doesn't start with a V");
                                    }
                                }
                                else
                                {
                                    throw new Exception("This MRN or ACCT is null.");
                                }                                
                            }                            
                        }
                        else
                        {
                            this.m_BackgroundWorker.ReportProgress(1, "There is no CDM for ReportNo/Code: " + reportNo.Value + " - " + panelSetOrderCPTCodeBill.CPTCode);
                            Business.Billing.Model.SVHNoCDMMailMessage.SendMessage(panelSetOrderCPTCodeBill.CPTCode);
                        }
                    }
                }                
            }
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            this.m_BackgroundWorker.ReportProgress(1, "Wrote " + rowCount + " SVH CDM files.");
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
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.ToString("MMddyyyy"));
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
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting transfer of PSA Files: " + DateTime.Now.ToLongTimeString());
            string configFilePath = @"C:\Program Files\Yellowstone Pathology Institute\psa-ssh-config.json";

            if (System.IO.File.Exists(configFilePath) == true)
            {
                JObject psaSSHConfig = null;
                using (StreamReader file = File.OpenText(configFilePath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    psaSSHConfig = (JObject)JToken.ReadFrom(reader);
                }

                string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathPSA, this.m_PostDate.ToString("MMddyyyy"));
                if(System.IO.Directory.Exists(workingFolder) == true)
                {
                    string[] files = System.IO.Directory.GetFiles(workingFolder);

                    Business.SSHFileTransfer sshFileTransfer = new Business.SSHFileTransfer(psaSSHConfig["host"].ToString(), Convert.ToInt32(psaSSHConfig["port"]),
                        psaSSHConfig["username"].ToString(), psaSSHConfig["password"].ToString());
                    sshFileTransfer.Failed += SshFileTransfer_Failed;

                    sshFileTransfer.StatusMessage += SSHFileTransfer_StatusMessage;                    
                    sshFileTransfer.UploadFilesToPSA(files);
                    rowCount += 1;
                }
                else
                {                    
                    this.m_BackgroundWorker.ReportProgress(1, "ERROR: The folder for this date does not exist.");
                }                
            }
            else
            {
                this.m_BackgroundWorker.ReportProgress(1, "ERROR: The PSA Config file could not be found.");                
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished with transfer of " + rowCount + " PSA Files: " + DateTime.Now.ToLongTimeString());
        }

        private void SshFileTransfer_Failed(object sender, string message)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Transfer of files to PSA has failed: " + DateTime.Now.ToLongTimeString());
        }

        private void SSHFileTransfer_StatusMessage(object sender, string message, int count)
        {
            this.m_StatusCount = count;
            this.m_BackgroundWorker.ReportProgress(1, message);
        }        

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {            
            this.m_StatusMessageList.Clear();            
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(AllProcessBackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(RunAllProcesses);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(AllProcessBackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void RunAllProcesses(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.RunUpdateMRNAcct(sender, e);
            this.MatchAccessionOrdersToADT(sender, e);            
            this.ProcessSVHCDMFiles(sender, e);
            this.TransferSVHFiles(sender, e);
            this.SendSVHClinicEmail(sender, e);            
            this.ProcessPSAFiles(sender, e);
            this.TransferPSAFiles(sender, e);
            this.FaxTheReport(sender, e);
        }

        private void AllProcessBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_StatusCount += 1;
                string message = (string)e.UserState;
                this.m_StatusMessageList.Insert(0, message);
                this.m_StatusCountMessage = this.m_StatusCount.ToString();                
                this.NotifyPropertyChanged("StatusCountMessage");
            }));
        }

        private void AllProcessBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTime.Today.Kind);
            var unixTimestamp = System.Convert.ToInt64((DateTime.Today - date).TotalSeconds);
            
            string logFileName = @"C:\ProgramData\ypi\BillingProcess" + unixTimestamp.ToString() + ".log";
            System.IO.StreamWriter streamWriter = new StreamWriter(logFileName);
            foreach(string line in this.ListViewStatus.Items)
            {
                streamWriter.WriteLine(line);
            }
            streamWriter.Flush();
            streamWriter.Close();

            MessageBox.Show("All done.");
        }

        private void MenuItemFaxReport_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(AllProcessBackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(FaxTheReport);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(AllProcessBackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();            
        }

        private void RunUpdateMRNAcct(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting Updating MRN/ACCT: " + DateTime.Now.ToLongTimeString());
            Business.Gateway.BillingGateway.UpdateMRNACCT();
            this.m_BackgroundWorker.ReportProgress(1, "Finished Updating MRN/ACCT: " + DateTime.Now.ToLongTimeString());
        }

        private void FaxTheReport(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                this.m_BackgroundWorker.ReportProgress(1, "Starting faxing report: " + DateTime.Now.ToLongTimeString());
                Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData clientBillingDetailReportData = YellowstonePathology.Business.Gateway.XmlGateway.GetClientBillingDetailReport(this.m_PostDate, this.m_PostDate, "1");
                YellowstonePathology.Document.ClientBillingDetailReportV2 clientBillingDetailReport = new Document.ClientBillingDetailReportV2(clientBillingDetailReportData, this.m_PostDate, this.m_PostDate);
                string tifPath = @"C:\ProgramData\ypi\SVH_BILLING_" + this.m_PostDate.Year + "_" + this.m_PostDate.Month + "_" + this.m_PostDate.Day + ".tif";
                Business.Helper.FileConversionHelper.SaveFixedDocumentAsTiff(clientBillingDetailReport.FixedDocument, tifPath);
                Business.ReportDistribution.Model.FaxSubmission.Submit("4062378090", "SVH Billing Report", tifPath);
                this.m_BackgroundWorker.ReportProgress(1, "Completed faxing report: " + DateTime.Now.ToLongTimeString());
            });            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.PostDate = this.m_PostDate.AddDays(-1);
            this.NotifyPropertyChanged("PostDate");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.PostDate = this.m_PostDate.AddDays(1);
            this.NotifyPropertyChanged("PostDate");
        }

        public void MatchAccessionOrdersToADT(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting matching SVH ADT: " + DateTime.Now.ToLongTimeString());
            List<Business.Billing.Model.AccessionListItem> accessionList = Business.Gateway.AccessionOrderGateway.GetSVHNotPosted();
            foreach (Business.Billing.Model.AccessionListItem accessionListItem in accessionList)
            {
                this.m_BackgroundWorker.ReportProgress(1, "Looking for a match for: " + accessionListItem.MasterAccessionNo);

                Business.ClientOrder.Model.ClientOrderCollection clientOrdersNeedingRegistration = Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(accessionListItem.MasterAccessionNo);
                Business.ClientOrder.Model.ClientOrder clientOrderNeedingRegistration = clientOrdersNeedingRegistration[0];

                Business.ClientOrder.Model.ClientOrderCollection possibleNewClientOrders = Business.Gateway.ClientOrderGateway.GetClientOrdersByPatientName(accessionListItem.PFirstName, accessionListItem.PLastName);
                foreach(Business.ClientOrder.Model.ClientOrder clientOrder in possibleNewClientOrders)
                {
                    if (clientOrder.OrderDate > clientOrderNeedingRegistration.OrderDate &&
                        clientOrder.PBirthdate == clientOrderNeedingRegistration.PBirthdate && 
                        clientOrder.ProviderName == "ANGELA DURDEN" &&
                        clientOrder.SvhMedicalRecord.StartsWith("V"))
                    {
                        Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(accessionListItem.MasterAccessionNo, this);
                        Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(accessionListItem.ReportNo);

                        foreach (Business.Test.PanelSetOrderCPTCodeBill psocb in pso.PanelSetOrderCPTCodeBillCollection)
                        {
                            this.m_BackgroundWorker.ReportProgress(1, "Found a match for: " + accessionListItem.MasterAccessionNo);
                            if (psocb.BillTo == "Client")
                            {
                                psocb.MedicalRecord = clientOrder.SvhMedicalRecord;
                                psocb.Account = clientOrder.SvhAccountNo;
                            }

                            if (psocb.PostDate.HasValue == false)
                                psocb.PostDate = this.m_PostDate;
                        }

                        Business.Persistence.DocumentGateway.Instance.Push(ao, this);
                        break;
                    }
                }
            }
            this.m_BackgroundWorker.ReportProgress(1, "Completed SVH ADT Matching: " + DateTime.Now.ToLongTimeString());
        }

        private void MenuItemMatchSVHADT_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(MatchAccessionOrdersToADT);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }
    }
}
