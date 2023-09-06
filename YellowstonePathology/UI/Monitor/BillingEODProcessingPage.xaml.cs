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
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Monitor
{
    /// <summary>
    /// Interaction logic for BillingEODProcessingPage.xaml
    /// </summary>
    public partial class BillingEODProcessingPage : UserControl, IMonitorPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private System.ComponentModel.BackgroundWorker m_BackgroundWorker;
        private DateTime m_PostDate;

        private string m_BaseWorkingFolderPathPSA = @"\\fileserver\Documents\Billing\PSA\";
        private string m_BaseWorkingFolderPathSVH = @"\\fileserver\Documents\Billing\SVH\";

        private ObservableCollection<string> m_StatusMessageList;
        private string m_StatusCountMessage;
        private int m_StatusCount;
        private List<string> m_ReportNumbersToProcess;

        private YellowstonePathology.Business.Billing.Model.EODProcessStatus m_EODProcessStatus;
        private bool m_IsStartedManually;

        public BillingEODProcessingPage()
        {
            this.m_ReportNumbersToProcess = new List<string>();
            this.m_StatusCount = 0;
            this.m_StatusMessageList = new ObservableCollection<string>();
            this.m_StatusMessageList.Add("No Status");

            this.m_PostDate = DateTime.Today;
            InitializeComponent();
            this.DataContext = this;
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            this.m_IsStartedManually = true;
            this.Start();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.PostDate = this.m_PostDate.AddDays(-1);
            this.TextBoxStartDate.Text = this.m_PostDate.ToShortDateString();
            this.NotifyPropertyChanged("PostDate");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.PostDate = this.m_PostDate.AddDays(1);
            this.TextBoxStartDate.Text = this.m_PostDate.ToShortDateString();
            this.NotifyPropertyChanged("PostDate");
        }

        public void Refresh()
        {
        }

        public void Start()
        {
            this.SetButtonVisibility();
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
            try
            {
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

                if (this.m_EODProcessStatus.ProcessPSAFiles.HasValue == false) this.ProcessPSAFiles(sender, e);
                else this.m_BackgroundWorker.ReportProgress(1, "Process PSA Files Already Performed: " + this.m_EODProcessStatus.ProcessPSAFiles.Value.ToLongTimeString());

                if (this.m_EODProcessStatus.TransferAPSFiles.HasValue == false) this.TransferAPSFiles(sender, e);
                else this.m_BackgroundWorker.ReportProgress(1, "Transfer APS Files Already Performed: " + this.m_EODProcessStatus.TransferPSAFiles.Value.ToLongTimeString());

                if (this.m_EODProcessStatus.FaxTheReport.HasValue == false) this.FaxTheReport(sender, e);
                else this.m_BackgroundWorker.ReportProgress(1, "Fax The Report Already Performed: " + this.m_EODProcessStatus.FaxTheReport.Value.ToLongTimeString());
            }
            catch(Exception execption)
            {
                this.m_BackgroundWorker.ReportProgress(1, execption.Message);

            }
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
            foreach (string line in this.ListViewStatus.Items)
            {
                streamWriter.WriteLine(line);
            }
            streamWriter.Flush();
            streamWriter.Close();

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void SendSVHClinicEmail(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Sending SVH Clinic Email: " + DateTime.Now.ToLongTimeString());
            int rowCount = Business.Billing.Model.SVHClinicMailMessage.SendMessage();
            this.m_BackgroundWorker.ReportProgress(1, "SVH Clinic Email Sent with " + rowCount.ToString() + " rows");
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "SendSVHClinicEmail");
        }

        private void TransferSVHFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting Transfer of SVH Files: " + DateTime.Now.ToLongTimeString());
            string destinationFolder = @"\\YPIIInterface2\ChannelData\Outgoing\1002";

            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.ToString("MMddyyyy"), "ft1");
            string[] files = System.IO.Directory.GetFiles(workingFolder);

            foreach (string file in files)
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

        private void ProcessPSAFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing PSA Files: " + DateTime.Now.ToLongTimeString());

            YellowstonePathology.Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersByPostDate(this.m_PostDate);
            string workingFolder = System.IO.Path.Combine(m_BaseWorkingFolderPathPSA, this.m_PostDate.ToString("MMddyyyy"));
            if (Directory.Exists(workingFolder) == false)
                Directory.CreateDirectory(workingFolder);

            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ProcessPSAFiles");
        }

        private void ProcessSVHCDMFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting processing SVH CDM files.");
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection hrhGroup = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("2");
            YellowstonePathology.Business.ReportNoCollection reportNoCollection = Business.Gateway.AccessionOrderGateway.GetReportNumbersBySVHProcess(this.m_PostDate);
            string workingFolder = System.IO.Path.Combine(this.m_BaseWorkingFolderPathSVH, this.m_PostDate.ToString("MMddyyyy"));
            if (Directory.Exists(workingFolder) == false)
            {
                Directory.CreateDirectory(workingFolder);
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "ft1"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "ft1", "done"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "result"));
                Directory.CreateDirectory(System.IO.Path.Combine(workingFolder, "result", "done"));
            }

            int rowCount = 0;
            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
                this.m_BackgroundWorker.ReportProgress(1, "Processing: " + reportNo.Value);

                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo.Value);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);

                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo.Value);
                foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in panelSetOrder.PanelSetOrderCPTCodeBillCollection)
                {
                    if (panelSetOrderCPTCodeBill.BillTo == "Client" && panelSetOrderCPTCodeBill.PostDate == this.m_PostDate)
                    {
                        if (YellowstonePathology.Business.Billing.Model.CDMCollection.Instance.Exists(panelSetOrderCPTCodeBill.CPTCode, "SVH") == true)
                        {
                            if (panelSetOrderCPTCodeBill.PostedToClient == false)
                            {
                                if (string.IsNullOrEmpty(panelSetOrderCPTCodeBill.MedicalRecord) == false && string.IsNullOrEmpty(panelSetOrderCPTCodeBill.Account) == false)
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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ProcessSVHCDMFiles");
        }

        private void CreateXmlBillingDocument(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            YellowstonePathology.Business.Billing.Model.PSABillingDocument psaBillingDocument = new YellowstonePathology.Business.Billing.Model.PSABillingDocument(accessionOrder, reportNo);
            psaBillingDocument.Build();

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
            string filePath = System.IO.Path.Combine(Business.Document.CaseDocumentPath.GetPath(orderIdParser), reportNo + ".BillingDetails.xml");
            psaBillingDocument.Save(filePath);
        }

        private void CopyFiles(string reportNo, string masterAccessionNo, string workingFolder)
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

        private void CreatePatientTifFile(string reportNo)
        {
            List<Business.Patient.Model.SVHBillingData> sVHBillingDataList = Business.Gateway.AccessionOrderGateway.GetPatientImportDataList(reportNo);
            if (sVHBillingDataList.Count != 0)
            {
                YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
                {
                    YellowstonePathology.Business.Document.SVHBillingDocument svhBillingDocument = new Business.Document.SVHBillingDocument(sVHBillingDataList[0]);
                    svhBillingDocument.SaveAsTIF(orderIdParser);
                }
                ));
            }
        }

        private void TransferAPSFiles(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int rowCount = 0;
            this.m_BackgroundWorker.ReportProgress(1, "Starting transfer of APS Files: " + DateTime.Now.ToLongTimeString());
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
                if (System.IO.Directory.Exists(workingFolder) == true)
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
                this.m_BackgroundWorker.ReportProgress(1, "ERROR: The PSA Config file could not be found.");
            }

            this.m_BackgroundWorker.ReportProgress(1, "Finished with transfer of " + rowCount + " PSA Files: " + DateTime.Now.ToLongTimeString());
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "TransferPSAFiles");
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

        private void RunUpdateMRNAcct(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.m_BackgroundWorker.ReportProgress(1, "Starting Updating MRN/ACCT: " + DateTime.Now.ToLongTimeString());
            Business.Gateway.BillingGateway.UpdateMRNACCT();
            this.m_BackgroundWorker.ReportProgress(1, "Finished Updating MRN/ACCT: " + DateTime.Now.ToLongTimeString());
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "MRNAcctUpdate");
        }

        private void FaxTheReport(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                this.m_BackgroundWorker.ReportProgress(1, "Starting faxing report: " + DateTime.Now.ToLongTimeString());
                Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData clientBillingDetailReportData = Business.Gateway.XmlGateway.GetClientBillingDetailReport(this.m_PostDate, this.m_PostDate, "1");
                YellowstonePathology.Document.ClientBillingDetailReportV2 clientBillingDetailReport = new Document.ClientBillingDetailReportV2(clientBillingDetailReportData, this.m_PostDate, this.m_PostDate);
                string tifPath = @"C:\ProgramData\ypi\SVH_BILLING_" + this.m_PostDate.Year + "_" + this.m_PostDate.Month + "_" + this.m_PostDate.Day + ".tif";
                Business.Helper.FileConversionHelper.SaveFixedDocumentAsTiff(clientBillingDetailReport.FixedDocument, tifPath);
                Business.ReportDistribution.Model.FaxSubmission.Submit("4062378090", "SVH Billing Report", tifPath, $"SVH Billing: {DateTime.Now.ToLongTimeString()}");
                this.m_BackgroundWorker.ReportProgress(1, "Completed faxing report: " + DateTime.Now.ToLongTimeString());
                Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "FaxTheReport");
            });
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
                foreach (Business.ClientOrder.Model.ClientOrder clientOrder in possibleNewClientOrders)
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
            Business.Gateway.BillingGateway.UpdateBillingEODProcess(this.m_PostDate, "ADTMatch");
        }

        private void HandleProcessStatus()
        {
            YellowstonePathology.Business.Gateway.BillingGateway.CreateBillingEODProcess(this.m_PostDate);
            this.m_EODProcessStatus = Business.Gateway.BillingGateway.GetBillingEODProcessStatus(this.m_PostDate);
        }

        private void SetButtonVisibility()
        {
            if (this.m_IsStartedManually == false)
            {
                this.ButtonBack.Visibility = Visibility.Hidden;
                this.ButtonForward.Visibility = Visibility.Hidden;
                this.ButtonStart.Visibility = Visibility.Hidden;
            }

        }

        private void ListViewStatus_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 1.0;

            gView.Columns[0].Width = workingWidth * col1;
        }
    }
}
