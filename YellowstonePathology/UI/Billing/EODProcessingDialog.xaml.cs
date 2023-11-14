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
using MySql.Data.MySqlClient;
using System.Data;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.UI.Billing
{    
    public partial class EODProcessingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        private System.ComponentModel.BackgroundWorker m_BackgroundWorker;
        private DateTime m_PostDate;
        
        private string m_BaseWorkingFolderPathPSA = @"\\fileserver\Documents\Billing\PSA\";
        private string m_BaseWorkingFolderPathSVH = @"\\fileserver\Documents\Billing\SVH\";
        private string m_BaseWorkingFolderPathAPS = @"\\fileserver\Documents\Billing\APS\";


        private ObservableCollection<string> m_StatusMessageList;
        private ObservableCollection<string> m_ErrorMessageList;

        private string m_StatusCountMessage;        
        private int m_StatusCount;
        private List<string> m_ReportNumbersToProcess;

        private Business.Billing.Model.EODProcessStatus m_EODProcessStatus;
        private Business.Billing.Model.EODProcessStatusCollection m_EODProcessStatusCollection;

        public EODProcessingDialog()
        {
            this.m_ReportNumbersToProcess = new List<string>();
            this.m_StatusCount = 0;
            this.m_StatusMessageList = new ObservableCollection<string>();
            this.m_StatusMessageList.Add("No Status");

            this.m_ErrorMessageList = new ObservableCollection<string>();
            this.m_ErrorMessageList.Add("No Errors");

            this.m_PostDate = DateTime.Today;
            this.m_EODProcessStatusCollection = Business.Gateway.BillingGateway.GetBillingEODProcessStatusHistory();
            InitializeComponent();
            this.DataContext = this;
            
            Closing += PSATransferDialog_Closing;
        }

        private void PSATransferDialog_Closing(object sender, CancelEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public string StatusCountMessage
        {
            get { return this.m_StatusCountMessage; }
        }        

        public ObservableCollection<string> StatusMessageList
        {
            get { return this.m_StatusMessageList; }
        }

        public ObservableCollection<string> ErrorMessageList
        {
            get { return this.m_ErrorMessageList; }
        }

        public DateTime PostDate
        {
            get { return this.m_PostDate; }
            set { this.m_PostDate = value; }
        }

        public Business.Billing.Model.EODProcessStatusCollection EODProcessStatusCollection
        {
            get { return this.m_EODProcessStatusCollection; }
        }
        
        private void MenuItemOpenAPSFolder_Click(object sender, RoutedEventArgs e)
        {
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathAPS, this.m_PostDate.ToString("MMddyyyy"));
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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "SendSVHClinicEmail");
        }        

        private void MenuItemProcessAPSFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(ProcessAPSFiles);
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
            string destinationFolder = @"\\YPIIInterface2\ChannelData\Outgoing\SCLHealth\";

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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "TransferSVHFiles");
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                this.m_StatusCount += 1;
                string message = (string)e.UserState;

                if(message.Contains("ERROR"))
                {
                    this.m_ErrorMessageList.Insert(0, message);
                }
                else
                {
                    this.m_StatusMessageList.Insert(0, message);
                    this.m_StatusCountMessage = this.m_StatusCount.ToString();
                    this.NotifyPropertyChanged("StatusCountMessage");
                }                       
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
			Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate);
            foreach (Business.ReportNo reportNo in reportNoCollection)
            {
				Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo.Value);
                string patientTifFilePath = Business.Document.CaseDocument.GetCaseFileNamePatientTif(orderIdParser);
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
        
        private void ProcessAPSFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing APS Files: " + DateTime.Now.ToLongTimeString());

            JArray dailyLogItems = new JArray();
            JObject dailyLog = new JObject(new JProperty("dailLog", dailyLogItems));                       

            Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate);
            string workingFolder = System.IO.Path.Combine(m_BaseWorkingFolderPathAPS, this.m_PostDate.ToString("MMddyyyy"));
            if (Directory.Exists(workingFolder) == false)
                Directory.CreateDirectory(workingFolder);

            foreach (Business.ReportNo reportNo in reportNoCollection)
            {                
                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);                

                if (accessionOrder.UseBillingAgent == true)
                {
                    if (panelSetOrder.IsBillable == true)
                    {
                        if (panelSetOrder.PanelSetOrderCPTCodeBillCollection.HasItemsToSendToPSA() == true)
                        {
                            JObject dailyLogItem = new JObject();
                            JProperty masterAccessionNoProperty = new JProperty("masterAccessionNo", masterAccessionNo);
                            JProperty reportNoProperty = new JProperty("reportNo", reportNo.Value);
                            dailyLogItem.Add(masterAccessionNoProperty);
                            dailyLogItem.Add(reportNoProperty);

                            this.m_ReportNumbersToProcess.Add(reportNo.Value);
                            this.CreatePatientTifFile(reportNo.Value);
                            this.CreateJSONBillingDocument(accessionOrder, reportNo.Value);

                            JArray documentsArray = new JArray();
                            List<string> files = null;
                            //if (accessionOrder.ClientId == 587)
                            //{
                                files = this.CopyFilesAPSPDF(reportNo.Value, accessionOrder.MasterAccessionNo, workingFolder);
                            //}
                            //else
                            //{
                            //    files = this.CopyFilesAPS(reportNo.Value, accessionOrder.MasterAccessionNo, workingFolder);
                            //}

                            foreach (string file in files)
                            {
                                documentsArray.Add(new JValue(file));
                            }
                            
                            this.m_BackgroundWorker.ReportProgress(1, reportNo.Value + " Complete.");
                            rowCount += 1;

                            dailyLogItem.Add(new JProperty("documents", documentsArray));
                            dailyLogItems.Add(dailyLogItem);
                        }
                    }
                }
            }

            string destinationFile = System.IO.Path.Combine(workingFolder, "DailyLog.json");

            using (StreamWriter file = File.CreateText(System.IO.Path.Combine(workingFolder, "DailyLog.json")))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                dailyLog.WriteTo(writer);
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished processing " + rowCount + " APS Files");
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ProcessAPSFiles");
        }

        private void ProcessAPSFilesSpecial(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing APS Files: " + DateTime.Now.ToLongTimeString());

            JArray dailyLogItems = new JArray();
            JObject dailyLog = new JObject(new JProperty("dailLog", dailyLogItems));

            string sql = "select pso.ReportNo, pso.MasterAccessionNo, BillTo, pb.PostDate " +
                "from tblPanelSetOrderCPTCodeBill pb " +
                "join tblPanelSetOrder pso on pb.ReportNo = pso.ReportNo " +
                "where pso.PanelSetId = 400 and pb.BillTo = 'Global';";

            Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersBySQL(sql);
            //string workingFolder = System.IO.Path.Combine(m_BaseWorkingFolderPathAPS, this.m_PostDate.ToString("MMddyyyy"));
            string workingFolder = @"c:\temp\aps_fix\";
            //if (Directory.Exists(workingFolder) == false)
            //    Directory.CreateDirectory(workingFolder);

            foreach (Business.ReportNo reportNo in reportNoCollection)
            {
                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);

                if (accessionOrder.UseBillingAgent == true)
                {
                    if (panelSetOrder.IsBillable == true)
                    {
                        
                        JObject dailyLogItem = new JObject();
                        JProperty masterAccessionNoProperty = new JProperty("masterAccessionNo", masterAccessionNo);
                        JProperty reportNoProperty = new JProperty("reportNo", reportNo.Value);
                        dailyLogItem.Add(masterAccessionNoProperty);
                        dailyLogItem.Add(reportNoProperty);

                        this.m_ReportNumbersToProcess.Add(reportNo.Value);
                        this.CreatePatientTifFile(reportNo.Value);
                        this.CreateJSONBillingDocument(accessionOrder, reportNo.Value);

                        JArray documentsArray = new JArray();
                        List<string> files = null;

                        //if(accessionOrder.ClientId == 587)
                        //{
                        files = this.CopyFilesAPSPDF(reportNo.Value, accessionOrder.MasterAccessionNo, workingFolder);
                        //}
                        //else
                        //{
                        //    files = this.CopyFilesAPS(reportNo.Value, accessionOrder.MasterAccessionNo, workingFolder);
                        //}
                        
                        foreach (string file in files)
                        {
                            documentsArray.Add(new JValue(file));
                        }

                        this.m_BackgroundWorker.ReportProgress(1, reportNo.Value + " Complete.");
                        rowCount += 1;

                        dailyLogItem.Add(new JProperty("documents", documentsArray));
                        dailyLogItems.Add(dailyLogItem);                        
                    }
                }
            }            

            using (StreamWriter file = File.CreateText(System.IO.Path.Combine(workingFolder, "DailyLog.json")))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                dailyLog.WriteTo(writer);
            }

            JArray o2 = (JArray)dailyLog["dailLog"];
            StringBuilder manRows = new StringBuilder();
            foreach (var item in o2.Children())
            {
                string man = item["masterAccessionNo"].ToString();
                manRows.AppendLine(man);
            }

            string manRowText = manRows.ToString();            
            using (StreamWriter manFile = File.CreateText(System.IO.Path.Combine(workingFolder, "master_accession_log.txt")))
            using (JsonTextWriter manWriter = new JsonTextWriter(manFile))
            {
                dailyLog.WriteTo(manWriter);
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished processing " + rowCount + " APS Files");
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ProcessAPSFiles");
        }

        private void ProcessSVHCDMFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing SVH CDM files.");
            //Business.Client.Model.ClientGroupClientCollection hrhGroup = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("2");
            Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersBySVHProcess(this.m_PostDate);
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.ToString("MMddyyyy"));
            if (Directory.Exists(workingFolder) == false)
            {
                Directory.CreateDirectory(workingFolder);
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "ft1"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "ft1", "done"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "result"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "result", "done"));
            }

            List<string> casesNotSent = new List<string>();
            int rowCount = 0;
            foreach (Business.ReportNo reportNo in reportNoCollection)
            {                
                this.m_BackgroundWorker.ReportProgress(1, "Processing: " + reportNo.Value);

                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);

                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);

                if (panelSetOrder.PanelSetId == 289) continue; //Don't send Autopsy's                
                foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in panelSetOrder.PanelSetOrderCPTCodeBillCollection)
                {
                    if (panelSetOrderCPTCodeBill.BillTo == "Client" && panelSetOrderCPTCodeBill.PostDate == this.m_PostDate)
                    {
                        if (Business.Billing.Model.CDMCollection.Instance.Exists(panelSetOrderCPTCodeBill.CPTCode, "SVH") == true)
                        {
                            if (panelSetOrderCPTCodeBill.PostedToClient == false)
                            {
                                if (string.IsNullOrEmpty(panelSetOrderCPTCodeBill.MedicalRecord) == false && string.IsNullOrEmpty(panelSetOrderCPTCodeBill.Account) == false)
                                {
                                    if (this.IsOKToSendSVHCDM(panelSetOrderCPTCodeBill, panelSetOrder) == true)
                                    {
                                        this.m_BackgroundWorker.ReportProgress(1, "Writing File: " + reportNo.Value + " - " + panelSetOrderCPTCodeBill.CPTCode);
                                        Business.HL7View.EPIC.EPICFT1ResultView epicFT1ResultView = new Business.HL7View.EPIC.EPICFT1ResultView(accessionOrder, panelSetOrderCPTCodeBill);
                                        epicFT1ResultView.Publish(System.IO.Path.Combine(workingFolder, "ft1"));
                                        panelSetOrderCPTCodeBill.PostedToClient = true;
                                        panelSetOrderCPTCodeBill.PostedToClientDate = DateTime.Now;
                                        panelSetOrder.BillingDelayed = false;
                                        rowCount += 1;
                                    }
                                    else
                                    {
                                        this.m_BackgroundWorker.ReportProgress(1, $"****CAUTION: Charges for {reportNo.Value} will not be sent across the interface. Test Name: {panelSetOrder.PanelSetName}.");
                                    }
                                }
                                else
                                {
                                    panelSetOrder.BillingDelayed = true;
                                    panelSetOrder.BillingDelayedComment = "Cannot process the SVH CDM because the MRN and/or ACCT is null.";                                    
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
            Business.Persistence.DocumentGateway.Instance.Push(this);
            this.m_BackgroundWorker.ReportProgress(1, "Wrote " + rowCount + " SVH CDM files.");
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ProcessSVHCDMFiles");
        }

        private bool IsOKToSendSVHCDM(Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill, Business.Test.PanelSetOrder panelSetOrder)
        {
            bool result = false;
            if (panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("V")) result = true;
            if (panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("R")) result = true;
            if (panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("X")) result = true;
            if (panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("C")) result = true;
            //if (panelSetOrder.PanelSetId == 400 && panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("A") == false) result = true;            
            if (panelSetOrder.PanelSetId == 400) result = true;            
            return result;
        }

        private void CreateXmlBillingDocument(Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            Business.Billing.Model.PSABillingDocument psaBillingDocument = new Business.Billing.Model.PSABillingDocument(accessionOrder, reportNo);
            psaBillingDocument.Build();

			Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo);
			string filePath = System.IO.Path.Combine(Business.Document.CaseDocumentPath.GetPath(orderIdParser), reportNo + ".BillingDetails.xml");
            psaBillingDocument.Save(filePath);
        }

        private void CreateJSONBillingDocument(Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            Business.Billing.Model.APSBillingDocument apsBillingDocument = new Business.Billing.Model.APSBillingDocument(accessionOrder, reportNo);
            apsBillingDocument.Build();

            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo);
            string filePath = System.IO.Path.Combine(Business.Document.CaseDocumentPath.GetPath(orderIdParser), reportNo + ".BillingDetails.json");
            using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, apsBillingDocument.Document);
            }
        }

        private void CopyFilesPSA(string reportNo, string masterAccessionNo, string workingFolder)
        {
            Business.Document.CaseDocumentCollection caseDocumentCollection = new Business.Document.CaseDocumentCollection(reportNo);
            Business.Document.CaseDocumentCollection billingCaseDocumentCollection = caseDocumentCollection.GetPsaFiles(reportNo, masterAccessionNo);

            foreach (Business.Document.CaseDocument caseDocument in billingCaseDocumentCollection)
            {                
                string sourceFile = caseDocument.FullFileName;
                string destinationFile = System.IO.Path.Combine(workingFolder, System.IO.Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile, true);
            }
        }

        private List<string> CopyFilesAPS(string reportNo, string masterAccessionNo, string workingFolder)
        {
            Business.Document.CaseDocumentCollection caseDocumentCollection = new Business.Document.CaseDocumentCollection(reportNo);
            
            Business.Document.CaseDocumentCollection billingCaseDocumentCollection = caseDocumentCollection.GetAPSFiles(reportNo, masterAccessionNo);
            List<string> result = new List<string>();

            foreach (Business.Document.CaseDocument caseDocument in billingCaseDocumentCollection)
            {
                string sourceFile = caseDocument.FullFileName;
                string destinationFile = System.IO.Path.Combine(workingFolder, System.IO.Path.GetFileName(sourceFile));
                string fileName = System.IO.Path.GetFileName(sourceFile);
                result.Add(System.IO.Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile, true);
            }
            return result;
        }

        private List<string> CopyFilesAPSPDF(string reportNo, string masterAccessionNo, string workingFolder)
        {
            Business.Document.CaseDocumentCollection caseDocumentCollection = new Business.Document.CaseDocumentCollection(reportNo);
            Business.Document.CaseDocumentCollection billingCaseDocumentCollection = caseDocumentCollection.GetAPSFilesPDF(reportNo, masterAccessionNo);
            List<string> result = new List<string>();

            foreach (Business.Document.CaseDocument caseDocument in billingCaseDocumentCollection)
            {
                string sourceFile = caseDocument.FullFileName;
                string destinationFile = System.IO.Path.Combine(workingFolder, System.IO.Path.GetFileName(sourceFile));
                string fileName = System.IO.Path.GetFileName(sourceFile);
                result.Add(System.IO.Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile, true);
            }
            return result;
        }

        private void CreatePatientTifFile(string reportNo)
        {
            List<Business.Patient.Model.SVHBillingData> sVHBillingDataList = Business.Gateway.AccessionOrderGateway.GetPatientImportDataList(reportNo);
			if (sVHBillingDataList.Count != 0)
            {
				Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo);
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
                {                    
					Business.Document.SVHBillingDocument svhBillingDocument = new Business.Document.SVHBillingDocument(sVHBillingDataList[0]);
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
			Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo);
			string wrongFile = System.IO.Path.Combine(Business.Document.CaseDocumentPath.GetPath(orderIdParser), orderIdParser.MasterAccessionNo + wrongName);
			string correctFile = System.IO.Path.Combine(Business.Document.CaseDocumentPath.GetPath(orderIdParser), orderIdParser.MasterAccessionNo + correctName);

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

        private void TransferAPSFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {            
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting transfer of APS Files: " + DateTime.Now.ToLongTimeString());
            string configFilePath = @"C:\Program Files\Yellowstone Pathology Institute\aps-ssh-config.json";

            if (System.IO.File.Exists(configFilePath) == true)
            {
                JObject psaSSHConfig = null;
                using (StreamReader file = File.OpenText(configFilePath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    psaSSHConfig = (JObject)JToken.ReadFrom(reader);
                }

                string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathAPS, this.m_PostDate.ToString("MMddyyyy"));
                if(System.IO.Directory.Exists(workingFolder) == true)
                {
                    string[] files = System.IO.Directory.GetFiles(workingFolder);
                    
                    Business.SSHFileTransfer sshFileTransfer = new Business.SSHFileTransfer(psaSSHConfig["host"].ToString(), Convert.ToInt32(psaSSHConfig["port"]),
                        psaSSHConfig["username"].ToString(), psaSSHConfig["password"].ToString());
                    sshFileTransfer.Failed += SshFileTransfer_Failed;

                    sshFileTransfer.StatusMessage += SSHFileTransfer_StatusMessage;                    
                    sshFileTransfer.UploadFilesToAPS(files);
                    rowCount += 1;
                }
                else
                {                    
                    this.m_BackgroundWorker.ReportProgress(1, "ERROR: The folder for this date does not exist.");
                }                
            }
            else
            {
                this.m_BackgroundWorker.ReportProgress(1, "ERROR: The APS Config file could not be found.");                
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished with transfer of " + rowCount + " APS Files: " + DateTime.Now.ToLongTimeString());
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "TransferAPSFiles");         
        }

        private void SshFileTransfer_Failed(object sender, string message)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Transfer of files to APS has failed: " + DateTime.Now.ToLongTimeString());
        }

        private void SSHFileTransfer_StatusMessage(object sender, string message, int count)
        {
            this.m_StatusCount = count;
            this.m_BackgroundWorker.ReportProgress(1, message);
        }        

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {            
            this.m_StatusMessageList.Clear();
            this.HandleProcessStatus();
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
            this.BillCOVIDCases(sender, e);

            if (this.m_EODProcessStatus.MRNAcctUpdate.HasValue == false) this.RunUpdateMRNAcct(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Updating MRN/ACCT Already Performed: " + this.m_EODProcessStatus.MRNAcctUpdate.Value.ToLongTimeString());

            if (this.m_EODProcessStatus.ADTMatch.HasValue == false) this.MatchAccessionOrdersToADT(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Matching SVH ADT Already Performed: " + this.m_EODProcessStatus.ADTMatch.Value.ToLongTimeString());

            if (this.m_EODProcessStatus.ProcessSVHCDMFiles.HasValue == false) this.ProcessSVHCDMFiles(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "SVH CDM files Already Performed: " + this.m_EODProcessStatus.ProcessSVHCDMFiles.Value.ToLongTimeString());

            if (this.m_EODProcessStatus.TransferSVHFiles.HasValue == false) this.TransferSVHFiles(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Transfer SVH Files Already Performed: " + this.m_EODProcessStatus.TransferSVHFiles.Value.ToLongTimeString());

            if (this.m_EODProcessStatus.SendSVHClinicEmail.HasValue == false) this.SendSVHClinicEmail(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Send SVH Clinic Email Already Performed: " + this.m_EODProcessStatus.SendSVHClinicEmail.Value.ToLongTimeString());            

            if (this.m_EODProcessStatus.ProcessAPSFiles.HasValue == false) this.ProcessAPSFiles(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Process APS Files Already Performed: " + this.m_EODProcessStatus.ProcessAPSFiles.Value.ToLongTimeString());            

            if (this.m_EODProcessStatus.TransferAPSFiles.HasValue == false) this.TransferAPSFiles(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Transfer APS Files Already Performed: " + this.m_EODProcessStatus.TransferAPSFiles.Value.ToLongTimeString());

            if (this.m_EODProcessStatus.FaxTheReport.HasValue == false) this.FaxTheReport(sender, e);
            else this.m_BackgroundWorker.ReportProgress(1, "Fax The Report Already Performed: " + this.m_EODProcessStatus.FaxTheReport.Value.ToLongTimeString());
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

            this.m_EODProcessStatusCollection = Business.Gateway.BillingGateway.GetBillingEODProcessStatusHistory();
            this.NotifyPropertyChanged("EODProcessStatusCollection");

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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "MRNAcctUpdate");
        }

        private void FaxTheReport(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                this.m_BackgroundWorker.ReportProgress(1, "Starting faxing report: " + DateTime.Now.ToLongTimeString());                
                Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData clientBillingDetailReportData = Business.Gateway.XmlGateway.GetClientBillingDetailReport(this.m_PostDate, this.m_PostDate, "1");
                YellowstonePathology.Document.ClientBillingDetailReportV2 clientBillingDetailReport = new Document.ClientBillingDetailReportV2(clientBillingDetailReportData, this.m_PostDate, this.m_PostDate);
                string tifPath = @"C:\ProgramData\ypi\SVH_BILLING_" + this.m_PostDate.Year + "_" + this.m_PostDate.Month + "_" + this.m_PostDate.Day + ".tif";
                Business.Helper.FileConversionHelper.SaveFixedDocumentAsTiff(clientBillingDetailReport.FixedDocument, tifPath);
                Business.ReportDistribution.Model.FaxSubmission.Submit("4062378090", "SVH Billing Report", tifPath, $"SCL BILLING: {DateTime.Now.ToLongTimeString()}");
                this.m_BackgroundWorker.ReportProgress(1, "Completed faxing report: " + DateTime.Now.ToLongTimeString());

                clientBillingDetailReportData = Business.Gateway.XmlGateway.GetClientBillingDetailReport(this.m_PostDate, this.m_PostDate, "2");
                clientBillingDetailReport = new Document.ClientBillingDetailReportV2(clientBillingDetailReportData, this.m_PostDate, this.m_PostDate);
                tifPath = @"C:\ProgramData\ypi\HRH_BILLING_" + this.m_PostDate.Year + "_" + this.m_PostDate.Month + "_" + this.m_PostDate.Day + ".tif";
                Business.Helper.FileConversionHelper.SaveFixedDocumentAsTiff(clientBillingDetailReport.FixedDocument, tifPath);
                Business.ReportDistribution.Model.FaxSubmission.Submit("4062332714", "HRH Billing Report", tifPath, $"HRH BILLING: {DateTime.Now.ToLongTimeString()}");
                this.m_BackgroundWorker.ReportProgress(1, "Completed faxing report: " + DateTime.Now.ToLongTimeString());                

                Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "FaxTheReport");                
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

                List<string> providerNameList = new List<string>();
                providerNameList.Add("KELLI SCHNEIDER");
                providerNameList.Add("ANGELA DURDEN");

                foreach (Business.ClientOrder.Model.ClientOrder clientOrder in possibleNewClientOrders)
                {
                    if (clientOrder.OrderDate > clientOrderNeedingRegistration.OrderDate &&
                        clientOrder.PBirthdate == clientOrderNeedingRegistration.PBirthdate && 
                        providerNameList.Contains(clientOrder.ProviderName) &&
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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ADTMatch");
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

        private void MenuItemUpdateMRNACT_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(RunUpdateMRNAcct);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void HandleProcessStatus()
        {
            Business.Gateway.BillingGateway.CreateBillingEODProcess(this.m_PostDate);
            this.m_EODProcessStatus = Business.Gateway.BillingGateway.GetBillingEODProcessStatus(this.m_PostDate);
        }

        private void MenuItemCheckForProblems_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(CheckForProblems);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void CheckForProblems(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.CheckForTifDoc(sender, e);
            //this.CheckSVHAcctNoStartsWithVorR(sender, e);
        }

        private void CheckForTifDoc(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Start Checking for TIF Files.");
            Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersByFinalDate(this.m_PostDate);
            foreach(Business.ReportNo reportNo in reportNoCollection)
            {
                //this.m_BackgroundWorker.ReportProgress(1, "Checking: " + reportNo.Value);
                if(Business.Document.CaseDocument.DoesCaseDocTifExist(reportNo.Value) == false ||                     
                    Business.Document.CaseDocument.DoesCaseDocPdfExist(reportNo.Value) == false ||
                    Business.Document.CaseDocument.DoesCaseDocDOCExist(reportNo.Value) == false ||
                    Business.Document.CaseDocument.DoesCaseDocXPSExist(reportNo.Value) == false)
                {
                    Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo.Value);
                    Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(orderIdParser.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);

                    if (panelSetOrder.HoldDistribution == false)
                    {
                        this.m_BackgroundWorker.ReportProgress(1, "One or more case documents is missing for ReportNo: " + reportNo.Value);
                    }

                    //Business.Interface.ICaseDocument caseDocument = Business.Document.DocumentFactory.GetDocument(accessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Normal);                    
                    //Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);
                    //caseDocument.Render();
                    //caseDocument.Publish();                
                }
            }
        }

        private void CheckSVHAcctNoStartsWithVorR(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting checking SVH AN - V or R.");
            Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersBySVHProcess(this.m_PostDate);

            foreach (Business.ReportNo reportNo in reportNoCollection)
            {
                this.m_BackgroundWorker.ReportProgress(1, "Checking: " + reportNo.Value);
                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);
                foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in panelSetOrder.PanelSetOrderCPTCodeBillCollection)
                {
                    if (panelSetOrderCPTCodeBill.BillTo == "Client" && 
                        panelSetOrderCPTCodeBill.PostDate == this.m_PostDate &&
                        string.IsNullOrEmpty(panelSetOrderCPTCodeBill.MedicalRecord) == false && 
                        panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("V") == false &&
                        panelSetOrderCPTCodeBill.MedicalRecord.StartsWith("R") == false)
                        {
                            this.m_BackgroundWorker.ReportProgress(1, "The MRN for this charge doesn't start with a V or R: " + reportNo.Value + " - " + panelSetOrderCPTCodeBill.CPTCode);
                        }
                }
                Business.Persistence.DocumentGateway.Instance.Push(this);
            }
        }

        private void MenuItemTransferAPSFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(TransferAPSFiles);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void BillStuff()
        {
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and ao.ClientId = 723 and isposted = false";
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and ao.ClientId = 137 and isposted = false";
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and ao.ClientId = 1759 and isposted = false";
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and ao.ClientId = 1758 and isposted = false";
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and ao.ClientId in (280,1134) and isposted = false";
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and ao.ClientId in (660,1556) and isposted = false";
            //string sql = "select distinct pso.masterAccessionNo from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo where pso.final = 1 and pso.PanelSetId = 400 and isposted = false";            

            //string sql = "select ao.MasterAccessionNo, ao.CLientName, pso.Final, ao.SvhMedicalRecord, ao.SvhAccount, pso.FinalDate " +
            //    "from tblAccessionOrder ao " +
            //    "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
            //    "where pso.PanelSetId = 400 and isPosted = 0 and SVHMedicalRecord like 'A%' and pso.Final = 1 and pso.Finaldate = '2020-11-17';";

            //string sql = "select distinct ao.MasterAccessionNo " +
            //    "from tblAccessionOrder ao " +
            //    "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
            //    "join tblPanelSetOrderCPTCodeBill psob on pso.ReportNo = psob.ReportNo " +
            //    "where pso.PanelSetId = 400 and isPosted = 1 and pso.Final = 1 and psob.postdate = '2021-01-15' and ao.CollectionDate <= '2020-12-31';";

            string sql = "select ao.MasterAccessionNo " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
                "where pso.PanelSetId = 400 and isPosted = 0 and pso.Final = 1;";                            
            
            List <Business.MasterAccessionNo> manList = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoListBySQL(sql);
            foreach (Business.MasterAccessionNo man in manList)
            {
                this.PostBilling(man.Value);
            }

            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public void MakeCDM()
        {
            string destinationFolder = @"d:\testing\svh\";

            string sql = "select ao.MasterAccessionNo " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
                "join tblPanelSetOrderCPTCodeBill psob on pso.ReportNo = psob.ReportNo " +
                "where pso.PanelSetId = 400 and isPosted = 1 and SVHMedicalRecord like 'A%' and pso.Final = 1 and psob.PostDate = '2020-12-31' order by pso.FinalDate; ";

            List<Business.MasterAccessionNo> manList = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoListBySQL(sql);
            foreach(Business.MasterAccessionNo man in manList)
            {
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(man.Value, this);
                Business.Test.SARSCoV2.SARSCoV2TestOrder sars = (Business.Test.SARSCoV2.SARSCoV2TestOrder)ao.PanelSetOrderCollection.GetPanelSetOrder(400);
                Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = sars.PanelSetOrderCPTCodeBillCollection[0];
                Business.HL7View.EPIC.EPICFT1ResultView result = new Business.HL7View.EPIC.EPICFT1ResultView(ao, panelSetOrderCPTCodeBill);
                result.Publish(destinationFolder);
            }                       
        }

        public void UnpostBilling(string masterAccessionNo)
        {
            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            Business.Test.SARSCoV2.SARSCoV2TestOrder sars = (Business.Test.SARSCoV2.SARSCoV2TestOrder)ao.PanelSetOrderCollection.GetPanelSetOrder(400);
            
            if(ao.PanelSetOrderCollection[0].NoCharge == false)
            {
                sars.IsPosted = false;
                sars.BillingType = null;

                sars.PanelSetOrderCPTCodeCollection.Clear();
                sars.PanelSetOrderCPTCodeBillCollection.Clear();
                

                //if (ao.PanelSetOrderCollection[0].PanelSetOrderCPTCodeBillCollection.Count > 0)
                //    ao.PanelSetOrderCollection[0].PanelSetOrderCPTCodeBillCollection.Remove(ao.PanelSetOrderCollection[0].PanelSetOrderCPTCodeBillCollection[0]);

                //if(ao.PanelSetOrderCollection[0].PanelSetOrderCPTCodeCollection.Count > 0)
                //    ao.PanelSetOrderCollection[0].PanelSetOrderCPTCodeCollection.Remove(ao.PanelSetOrderCollection[0].PanelSetOrderCPTCodeCollection[0]);
            }

            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public void PostBilling(string masterAccessionNo)
        {
            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            Business.Test.SARSCoV2.SARSCoV2TestOrder sars = (Business.Test.SARSCoV2.SARSCoV2TestOrder)ao.PanelSetOrderCollection.GetPanelSetOrder(400);

            Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(ao, sars.ReportNo);
            Business.Rules.MethodResult methodResult = billableObject.Set();
            if (methodResult.Success == false)
            {
                MessageBox.Show(methodResult.Message);
            }
            
            Business.Rules.MethodResult methodResult2 = billableObject.Post();
            if (methodResult2.Success == false)
            {
                MessageBox.Show(methodResult2.Message);
            }
        }        

        private void ButtonBillStuff_Click(object sender, RoutedEventArgs e)
        {
            /*
            string text = System.IO.File.ReadAllText(@"c:\temp\savedcovid.txt");
            string[] lines = text.Split(
              new[] { "\r\n", "\r", "\n" },
              StringSplitOptions.None
            );

            foreach (string line in lines)
            {
                //Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(line, this);
                //ao.UseBillingAgent = true;
                this.UnpostBilling(line);
            }

            Business.Persistence.DocumentGateway.Instance.Push(this);
            */

            //this.MakeCDM();
            //this.BillStuff();
            //ImportSVHCOVID();
            //ImportSVHCOVIDTab();
            //CheckAPSFiles();
        }

        private void CheckAPSFiles()
        {
            DateTime postDate = DateTime.Parse("12/7/2020");
            string sql = "SELECT distinct pso.MasterAccessionNo " +
                "FROM tblAccessionOrder a  " +
                "JOIN tblPanelSetOrder pso ON a.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelSetOrderCPTCodeBill psocpt on pso.ReportNo = psocpt.ReportNo " +                
                $"WHERE pso.IsPosted = 1 and psocpt.PostDate = '{postDate.ToString("yyyy-MM-dd")}' and billTo <> 'Client';";

            string folder = $@"\\fileserver\Documents\Billing\APS\{postDate.ToString("MMddyyyy")}";
            string [] files = System.IO.Directory.GetFiles(folder);

            List<Business.MasterAccessionNo> manList = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoListBySQL(sql);
            List<string> notFoundList = new List<string>();

            foreach (Business.MasterAccessionNo man in manList)
            {
                bool manFound = false;
                foreach (string file in files)
                {
                    if(file.Contains(man.Value) == true)
                    {
                        manFound = true;
                        break;                        
                    }                    
                }
                if (manFound == false) notFoundList.Add(man.Value);
                Console.WriteLine($"{man.Value} was found is: {manFound}");
            }            
        }

        private void WriteCDMFiles(string masterAccessionNo)
        {
            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(400);

            if (pso.PanelSetOrderCPTCodeBillCollection.Count != 1) throw new Exception("PSOB count is wrong.");
            Business.HL7View.EPIC.EPICFT1ResultView epicFT1ResultView = new Business.HL7View.EPIC.EPICFT1ResultView(ao, pso.PanelSetOrderCPTCodeBillCollection[0]);
            epicFT1ResultView.Publish(@"c:\temp\svh_cdm\");

            Business.Persistence.DocumentGateway.Instance.Push(ao, this);
        }

        private void AdjustPostDate(string masterAccessionNo, DateTime postDate)
        {
            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(400);

            foreach (Business.Test.PanelSetOrderCPTCode psoc in pso.PanelSetOrderCPTCodeCollection)
            {
                psoc.PostDate = postDate;
            }

            foreach (Business.Test.PanelSetOrderCPTCodeBill psocb in pso.PanelSetOrderCPTCodeBillCollection)
            {                           
                psocb.PostDate = postDate;
            }
            Business.Persistence.DocumentGateway.Instance.Push(ao, this);
        }

        private void ImportSVHCOVIDTab()
        {
            string fileName = @"c:\temp\edie1207.txt";
            string text = System.IO.File.ReadAllText(fileName);
            string[] newLine = new string[1] { "\r\n" };
            string[] lineSplit = text.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lineSplit)
            {
                string[] commaSplit = line.Split('\t');
                string masterAccessionNo = commaSplit[3];
                //this.PostBilling(masterAccessionNo);
                //this.AdjustPostDate(masterAccessionNo, DateTime.Parse("12/7/2021"));
                this.WriteCDMFiles(masterAccessionNo);
            }            
        }        

        private void ImportSVHCOVID()
        {
            string fileName = @"c:\temp\ypibilling.csv";
            string text = System.IO.File.ReadAllText(fileName);
            string[] newLine = new string[1] { "\r\n" };
            string[] lineSplit = text.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lineSplit)
            {
                string[] commaSplit = line.Split(',');
                string masterAccessionNo = commaSplit[3];
                string aNumber = commaSplit[4];
                string vNumber = commaSplit[6];
                string lastName = commaSplit[7];
                string accountNumber = "";

                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
                Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(400);
                foreach (Business.Test.PanelSetOrderCPTCodeBill psocb in pso.PanelSetOrderCPTCodeBillCollection)
                {
                    if (psocb.BillTo == "Client")
                    {
                        psocb.MedicalRecord = vNumber;
                        psocb.Account = accountNumber;
                    }

                    if (psocb.PostDate.HasValue == false)
                        psocb.PostDate = this.m_PostDate;
                }

                Business.Persistence.DocumentGateway.Instance.Push(ao, this);
            }
        }

        private void MenuItemProcessAPSFixFiles_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(ProcessAPSFilesSpecial);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void MenuItemBillCOVIDCases_Click(object sender, RoutedEventArgs e)
        {
            this.m_StatusMessageList.Clear();
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(BillCOVIDCases);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void BillCOVIDCases(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, $"Starting to Bill COVID cases: {DateTime.Now.ToLongTimeString()}");

            Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate);

            string sql = "select ao.MasterAccessionNo " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
                "where pso.PanelSetId in (400,415) and isPosted = 0 and pso.Final = 1;";

            List<Business.MasterAccessionNo> manList = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoListBySQL(sql);
            foreach (Business.MasterAccessionNo man in manList)
            {
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(man.Value, this);
                //Business.Test.SARSCoV2.SARSCoV2TestOrder sars = (Business.Test.SARSCoV2.SARSCoV2TestOrder)ao.PanelSetOrderCollection.GetPanelSetOrder(415);
                Business.Test.PanelSetOrder sars = ao.PanelSetOrderCollection.GetFirstCOVIDTest();

                Business.Billing.Model.BillableObject billableObject = Business.Billing.Model.BillableObjectFactory.GetBillableObject(ao, sars.ReportNo);
                Business.Rules.MethodResult methodResult = billableObject.Set();
                if (methodResult.Success == false)
                {
                    this.m_BackgroundWorker.ReportProgress(1, $"Failed billing COVID case: {sars.ReportNo}");
                }

                Business.Rules.MethodResult methodResult2 = billableObject.Post();
                if (methodResult2.Success == false)
                {
                    this.m_BackgroundWorker.ReportProgress(1, $"Failed billing COVID case: {sars.ReportNo}");
                }

                this.m_BackgroundWorker.ReportProgress(1, $"Finished billing COVID case: {sars.ReportNo}");
                rowCount += 1;
            }

            Business.Persistence.DocumentGateway.Instance.Push(this);

            this.m_BackgroundWorker.ReportProgress(1, $"Finished billing COVID cases: {rowCount} COVID Cases");
            //Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ProcessPSAFiles");
        }

        private void MenuItemProcessSVHCDMFiles_Click(object sender, RoutedEventArgs e)
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

        private void MenuItemSpecialTransfer_Click(object sender, RoutedEventArgs e)
        {
            string configFilePath = @"C:\Program Files\Yellowstone Pathology Institute\aps-ssh-config.json";

            JObject psaSSHConfig = null;
            using (StreamReader file = File.OpenText(configFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                psaSSHConfig = (JObject)JToken.ReadFrom(reader);
            }

            string sql = "select reportNo from tblPanelSetOrder where panelsetid in (378,379) and orderDate >= '2022-01-11'";
            Business.ReportNoCollection reportNos = Business.Gateway.AccessionOrderGateway.GetReportNumbers(sql);

            List<string> fileList = new List<string>();
            foreach (Business.ReportNo reportNo in reportNos)
            {
                string path = Business.Document.CaseDocumentPath.GetPath(new Business.OrderIdParser(reportNo.Value));                
                string pdf = $"{path}\\{reportNo.Value}.pdf";
                string json = $"{path}\\{reportNo.Value}.BillingDetails.json";
                fileList.Add(pdf);
                fileList.Add(json);
            }

            //Business.SSHFileTransfer sshFileTransfer = new Business.SSHFileTransfer(psaSSHConfig["host"].ToString(), Convert.ToInt32(psaSSHConfig["port"]),
            //            psaSSHConfig["username"].ToString(), psaSSHConfig["password"].ToString());
            //sshFileTransfer.Failed += SshFileTransfer_Failed;

            //sshFileTransfer.StatusMessage += SSHFileTransfer_StatusMessage;
            string[] files = fileList.ToArray<string>();

            foreach(string file in files)
            {
                if(System.IO.File.Exists(file) == false)
                {
                    Console.WriteLine(file);
                }
            }
            //sshFileTransfer.UploadFilesToAPS(files);
        }

        public void HandleSvhErrors()
        {            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select pso.ReportNo, f.quantity, f.Mrn, f.cptCode, count(*) - 1 " +
                "from tblFt1Log f " +
                "join tblAccessionOrder ao on f.MasterAccessionNo = ao.MasterAccessionNo " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.masterAccessionNo " +
                "where exists(Select null from tblPanelSetOrderCPTCodeBill where reportNo = pso.reportNo and cptcode = f.cptcode) and date(f.datesent) = '2023-10-27' " +
                "group by pso.reportNo, f.quantity, f.mrn, f.cptcode having count(*) > 1;";

            cmd.CommandType = CommandType.Text;            
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new { reportNo = dr.GetString(0), quantity = dr.GetInt32(1), cptCode = dr.GetString(3), count = dr.GetInt32(4) };
                        string masterAccessionNo = item.reportNo.Split('.')[0];
                        AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
                        PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(item.reportNo);
                        PanelSetOrderCPTCodeBill psob = null;
                        if(pso.PanelSetOrderCPTCodeBillCollection.Count == 1)
                        {
                            psob = pso.PanelSetOrderCPTCodeBillCollection.GetByCPTCode(item.cptCode);
                        }
                        else
                        {
                            psob = pso.PanelSetOrderCPTCodeBillCollection.GetByCPTCodeAndModifier(item.cptCode, "TC");
                            if(psob == null)
                            {
                                psob = pso.PanelSetOrderCPTCodeBillCollection.GetByCPTCode(item.cptCode);
                            }
                        }

                        psob.Quantity = (item.count * -1) * item.quantity;
                        Business.HL7View.EPIC.EPICFT1ResultView epicFT1ResultView = new Business.HL7View.EPIC.EPICFT1ResultView(ao, psob);
                        epicFT1ResultView.Publish(System.IO.Path.Combine("c:\\temp\\", "ft1"));           
                    }
                }
            }            
        }

        private void MenuItemAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.HandleSvhErrors();
        }
    }
}
