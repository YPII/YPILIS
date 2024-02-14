using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using StackExchange.Redis;

namespace YellowstonePathology.UI.ReportDistribution
{    
    public partial class ReportDistributionWorkspace : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        
        
        private TimeSpan m_TimerInterval= TimeSpan.FromSeconds(30);
        private TimeSpan m_TimerIntervalStart = TimeSpan.FromSeconds(1);
        private System.Windows.Threading.DispatcherTimer m_TimerThatWorks;

        private ObservableCollection<string> m_LogList;                

        public ReportDistributionWorkspace()
        {            
            this.m_LogList = new ObservableCollection<string>();            

            this.m_TimerThatWorks = new System.Windows.Threading.DispatcherTimer();
            this.m_TimerThatWorks.Interval = TimeSpan.FromSeconds(5);
            this.m_TimerThatWorks.Tick += TimerThatWorks_Tick;
            this.m_TimerThatWorks.Start();
            this.AddToLog("Starting the timer.");

            InitializeComponent();

            this.DataContext = this;            
        }

        private void TimerThatWorks_Tick(object sender, EventArgs e)
        {
            this.AddToLog($"Timer Tick at {DateTime.Now.ToShortTimeString()}");            
            this.m_TimerThatWorks.Interval = this.m_TimerInterval;

            DateTime dailyStartTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 05:00");
            DateTime dailyEndTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 23:50");
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("ReportDistributionHeartBeat", "Hello");
            
            //if (DateTime.Now >= dailyStartTime && DateTime.Now <= dailyEndTime)
            //{
                this.m_TimerThatWorks.Stop();
                
                this.HandleUnscheduledAmendments();
                this.HandleUnscheduledGlobalAmendments();
                this.HandleUnsetDistribution(); 
                this.HandleUnscheduledPublish();
                this.HandleUnscheduledDistribution();
                this.PublishNext();
                
                this.m_TimerThatWorks.Interval = this.m_TimerInterval;
                this.m_TimerThatWorks.Start();                
            //}
            //else
            //{                
            //    this.m_TimerThatWorks.Interval = this.m_TimerInterval;
            //    this.AddToLog("Idle due to after hours.");
           // }

            this.AddToLog($"System is idle now for {m_TimerInterval} seconds.");
        }        

        public ObservableCollection<string> LogList
        {
            get { return this.m_LogList; }
        }

        private void AddToLog(string text)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_LogList.Insert(0, text);
                if (this.m_LogList.Count > 200)
                {
                    this.m_LogList.RemoveAt(this.m_LogList.Count - 1);
                }
                this.NotifyPropertyChanged(string.Empty);
            }));            
        }                

        private void HandleUnscheduledAmendments()
        {
            this.AddToLog("Handling Unscheduled Amendments.");
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledAmendments();
            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
                foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                {
                    YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = accessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
                    foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                    {
                        foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                        {
                            if (testOrderReportDistribution.TimeOfLastDistribution < amendment.FinalTime && testOrderReportDistribution.ScheduledDistributionTime == null)
                            {
                                this.ScheduleDistribution(testOrderReportDistribution, panelSetOrder);
                            }
                        }
                    }
                }
            }

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);  
        }

        private void HandleUnscheduledGlobalAmendments()
        {
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledGlobalAmendments();
            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
                foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                {
                    YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = accessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
                    foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                    {
                        foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                        {
                            if (testOrderReportDistribution.TimeOfLastDistribution < amendment.FinalTime && testOrderReportDistribution.ScheduledDistributionTime == null)
                            {
                                this.ScheduleDistribution(testOrderReportDistribution, panelSetOrder);
                            }
                        }
                    }
                }
            }

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void HandleUnsetDistribution()
        {
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = Business.Gateway.AccessionOrderGateway.GetCasesWithUnsetDistributions();

            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);				

                if(accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                    {
                        if (panelSetOrder.Final == true && panelSetOrder.Distribute == true)
                        {
                            if (panelSetOrder.TestOrderReportDistributionCollection.Count == 0)
                            {
                                YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(accessionOrder.PhysicianId, accessionOrder.ClientId);
                                bool canSetDistribution = false;
                                string reason = "No Distribution Defined";
                                if (physicianClientDistributionCollection.Count != 0)
                                {
                                    YellowstonePathology.Business.Audit.Model.CanSetDistributionAudit canSetDistributionAudit = new Business.Audit.Model.CanSetDistributionAudit(accessionOrder, physicianClientDistributionCollection);
                                    canSetDistributionAudit.Run();
                                    if (canSetDistributionAudit.Status == Business.Audit.Model.AuditStatusEnum.OK)
                                    {
                                        physicianClientDistributionCollection.SetDistribution(panelSetOrder, accessionOrder);
                                        this.AddToLog($"Handling unset distribution for {panelSetOrder.ReportNo}");                                        
                                        canSetDistribution = true;
                                    }
                                    else
                                    {
                                        reason = "Missing MRN and/or AccountNo.";
                                    }
                                }
                                if(canSetDistribution == false)
                                {
                                    panelSetOrder.DistributionDelayed = true;
                                    panelSetOrder.DistributionDelayedComment = $"{reason}: {panelSetOrder.ReportNo}";

                                    /*
                                    this.AddToLog($"ERROR: not able to handle unset distribution for: {panelSetOrder.ReportNo}");
                                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Support@ypii.com", "Support@ypii.com", System.Windows.Forms.SystemInformation.UserName, reason + ": " + panelSetOrder.ReportNo);
                                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.110 ");

                                    Uri uri = new Uri("http://tempuri.org/");
                                    System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                                    System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                                    client.Credentials = credential;
                                    client.Send(message);
                                    */
                                }
                            }
                        }
                    }
                }                
            }

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void HandleUnscheduledDistribution()
        {
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledDistributions();
            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
                if(accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                    {
                        if (panelSetOrder.Final == true && panelSetOrder.Distribute == true && panelSetOrder.HoldDistribution == false)
                        {
                            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                            {
                                if (testOrderReportDistribution.Distributed == false && testOrderReportDistribution.ScheduledDistributionTime == null)
                                {
                                    this.ScheduleDistribution(testOrderReportDistribution, panelSetOrder);
                                }
                            }                            
                        }                        
                    }
                }                
            }

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void ScheduleDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {                                                  
            testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(10);
            panelSetOrder.Published = false;
            testOrderReportDistribution.Distributed = false;            
            panelSetOrder.ScheduledPublishTime = DateTime.Now.AddMinutes(10);            
            this.AddToLog($"INFO: Secheduling distribution for {panelSetOrder.ReportNo}");
        }        

        private void HandleUnscheduledPublish()
        {
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledPublish();

            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {                
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
                if(accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                    {
                        if (panelSetOrder.Final == true && panelSetOrder.ScheduledPublishTime == null && panelSetOrder.Published == false)
                        {
                            DateTime scheduleTime = DateTime.Now;
                            if (panelSetOrder.FinalTime > DateTime.Now.AddMinutes(-15))
                            {
                                scheduleTime = panelSetOrder.FinalTime.Value.AddMinutes(15);
                            }

                            panelSetOrder.ScheduledPublishTime = scheduleTime;                            
                            this.AddToLog($"Scheduling publish for case {panelSetOrder.ReportNo}");

                            if (panelSetOrder.Distribute == true)
                            {
                                foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                                {
                                    if (testOrderReportDistribution.Distributed == false && testOrderReportDistribution.ScheduledDistributionTime == null)
                                    {
                                        //this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unschedule Publish", testOrderReportDistribution.DistributionType, panelSetOrder.ReportNo,
                                        //panelSetOrder.MasterAccessionNo, testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "TestOrderReportDistribution Sceduled");
                                        this.AddToLog($"Scheduling publish for case {panelSetOrder.ReportNo}.");

                                        testOrderReportDistribution.ScheduledDistributionTime = scheduleTime;
                                    }
                                }
                            }
                        }
                    }
                }                
            }

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private bool TryPublish(YellowstonePathology.Business.Interface.ICaseDocument caseDocument, Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            bool result = true;

            //try
            //{                
                YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum resultDocumentSource;
                bool hasEnum = Enum.TryParse<YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum>(panelSetOrder.ResultDocumentSource, out resultDocumentSource);


                if (panelSetOrder.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase.ToString())
                {
                    caseDocument.Render();
                    caseDocument.Publish();
                }
                        
                this.AddToLog($"Published case: {panelSetOrder.ReportNo} - {panelSetOrder.PanelSetName}.");

            //}                                    
            //catch (Exception publishException)
            //{
                //this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo, null, null, publishException.Message);                
            //    this.DelayPublishAndDistribution(15, publishException.Message, panelSetOrder);
            //    result = false;
            //}                                    

            return result;
        }

        public bool TryDelete(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Interface.ICaseDocument caseDocument,
			YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            bool result = true;            
            YellowstonePathology.Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);

            if (methodResult.Success == false)
            {
                throw new Exception($"{panelSetOrder.ReportNo}: Not able to delete files prior to publishing.  Case will be delayed.");                
            }

            return result;
        }

        private void DelayPublishAndDistribution(int delayMinutes, string delayMessage, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            panelSetOrder.Published = false;
            panelSetOrder.TimeLastPublished = null;
            panelSetOrder.ScheduledPublishTime = DateTime.Now.AddMinutes(delayMinutes);            

            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = Business.Gateway.AccessionOrderGateway.GetScheduledDistribution(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
            {                
                testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(delayMinutes);
                testOrderReportDistribution.Rescheduled = true;
                testOrderReportDistribution.RescheduledMessage = delayMessage;                                
            }
        }

        private void PublishNext()
        {
            List<YellowstonePathology.Business.Test.PanelSetOrderView> caseList = Business.Gateway.AccessionOrderGateway.GetNextCasesToPublish();

            this.AddToLog($"Found {caseList.Count} cases that need to be published.");
            int maxProcessCount = 10;
            if (caseList.Count >= 10) maxProcessCount = 30;

            int processCount = 0;

            foreach (YellowstonePathology.Business.Test.PanelSetOrderView view in caseList)
            {
                try
                {
                    YellowstonePathology.WorkingReportNo.Instance.ReportNo = view.ReportNo;

                    YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("ReportDistributionHeartBeat", "Hello");
                    this.AddToLog($"Attempting publish of: {view.ReportNo}");
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(view.MasterAccessionNo, this);
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(view.ReportNo);

                    if (panelSetOrder.PanelSetId == 116) this.HandleWHP(accessionOrder, panelSetOrder);

                    YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

                    YellowstonePathology.Business.Interface.ICaseDocument caseDocument = Business.Document.DocumentFactory.GetDocument(accessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
                    YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(panelSetOrder.ReportNo);

                    this.AddToLog($"Hold distribution for: {view.ReportNo} is {panelSetOrder.HoldDistribution}");
                    if (panelSetOrder.HoldDistribution == false && panelSetOrder.DistributionDelayed == false)
                    {
                        if (this.TryDelete(panelSetOrder, caseDocument, orderIdParser) == true)
                        {
                            if (this.TryPublish(caseDocument, accessionOrder, panelSetOrder) == true)
                            {
                                if (panelSetOrder.Distribute == true)
                                {
                                    foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                                    {
                                        if (testOrderReportDistribution.Distributed == false)
                                        {
                                            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = this.Distribute(testOrderReportDistribution, accessionOrder, panelSetOrder);
                                            if (distributionResult.IsComplete == true)
                                            {
                                                testOrderReportDistribution.TimeOfLastDistribution = DateTime.Now;
                                                testOrderReportDistribution.ScheduledDistributionTime = null;
                                                testOrderReportDistribution.Distributed = true;

                                                string testOrderReportDistributionLogId = Guid.NewGuid().ToString();
                                                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                                                YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog testOrderReportDistributionLog = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLog(testOrderReportDistributionLogId, objectId);
                                                testOrderReportDistributionLog.FromTestOrderReportDistribution(testOrderReportDistribution);
                                                testOrderReportDistributionLog.TimeDistributed = DateTime.Now;
                                                panelSetOrder.TestOrderReportDistributionLogCollection.Add(testOrderReportDistributionLog);

                                                this.AddToLog($"Publishing case: {panelSetOrder.ReportNo} - {panelSetOrder.PanelSetName}");
                                            }
                                            else
                                            {
                                                testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(30);
                                                testOrderReportDistribution.Rescheduled = true;
                                                testOrderReportDistribution.RescheduledMessage = distributionResult.Message;

                                                this.AddToLog($"ERROR publishing case: {panelSetOrder.ReportNo} - {panelSetOrder.PanelSetName}");

                                                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Sid.Harder@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, distributionResult.Message);
                                                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.110 ");

                                                Uri uri = new Uri("http://tempuri.org/");
                                                System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                                                System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                                                client.Credentials = credential;
                                                client.Send(message);
                                            }
                                        }
                                    }
                                }

                                panelSetOrder.Published = true;
                                panelSetOrder.TimeLastPublished = DateTime.Now;
                                panelSetOrder.ScheduledPublishTime = null;

                                Business.Persistence.DocumentGateway.Instance.Save();
                            }
                        }

                        processCount += 1;
                        if (processCount == maxProcessCount) break;
                    }

                }
                catch (Exception exception)
                {
                    Business.Logging.EmailExceptionHandler.HandleException(exception.Message, WorkingReportNo.Instance.ReportNo);
                    Business.Gateway.AccessionOrderGateway.SetPanelSetOrderAsDelayedDistribution(WorkingReportNo.Instance.ReportNo, "Distribution failed in PublishNext loop.");
                }
                finally
                {
                    YellowstonePathology.WorkingReportNo.Instance.ReportNo = null;
                }
                                
            }                
            
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }        

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult Distribute(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = null;
            this.AddToLog($"Distributing: {testOrderReportDistribution.ReportNo} - {testOrderReportDistribution.DistributionType}, {accessionOrder.ClientName}");
            switch (testOrderReportDistribution.DistributionType)
            {
                case YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX:
                case "EPIC->Fax":
                case "Meditech->Fax":
                case "Athena->Fax":
                case "Athena Health->Fax":
                case "Web Service and Fax":
                case "ECW->Fax":
                    result = this.HandleFaxDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.Client.Model.EPICPhysicianClientDistribution.EPIC:
                case "EPIC and Fax":
                    result = this.HandleEPICDistribution(testOrderReportDistribution, accessionOrder, panelSetOrder);                    
                    break;
                case YellowstonePathology.Business.Client.Model.ECWRiverstonePhysicianClientDistribution.ECWRIVERSTONE:
                    result = this.HandleECWRiverstoneDistribution(testOrderReportDistribution, accessionOrder);
                    break;
                case YellowstonePathology.Business.Client.Model.ECWPhysicianClientDistribution.ECW:
                    result = this.HandleECWDistribution(testOrderReportDistribution, accessionOrder);
                    break;
                case YellowstonePathology.Business.Client.Model.AthenaPhysicianClientDistribution.ATHENA:
                    result = this.HandleATHENADistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case YellowstonePathology.Business.Client.Model.MediTechPhysicianClientDistribution.MEDITECH:
                    result = this.HandleMeditechDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case YellowstonePathology.Business.Client.Model.MediTechNMHPhysicianClientDistribution.MEDITECHNMH:
                    result = this.HandleMeditechNMHDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case YellowstonePathology.Business.Client.Model.EMAPhysicianClientDistribution.EMA:
                    result = this.HandleEMADistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case YellowstonePathology.Business.Client.Model.WebServicePhysicianClientDistribution.WEBSERVICE:
                    result = this.HandleWebServiceDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.Client.Model.PrintPhysicianClientDistribution.PRINT:
                    result = this.HandlePrintDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.Client.Model.MTDOHPhysicianClientDistribution.MTDOH:
                    result = this.HandleMTDOHDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;                
                case "Text":
                    result = this.HandleTextDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case "Email":
                    result = this.HandleEmailDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case "Text and Email":
                    result = this.HandleTextAndEmailDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                default:
                    result = this.HandleNoImplemented(testOrderReportDistribution);
                    break;
            }            
            
            if(result != null && result.IsComplete == false)
            {
                Business.Logging.EmailExceptionHandler.HandleException(result.Message);
                throw new Exception(result.Message);
            }

            return result;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleTextDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            Business.ReportDistribution.Model.DistributionResult distributionResult = Business.ReportDistribution.Model.TextSubmission.Submit(accessionOrder.PPhoneNumberHome, reportNo, accessionOrder.PFirstName);                        
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleEmailDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            Business.ReportDistribution.Model.DistributionResult distributionResult = Business.ReportDistribution.Model.EmailSubmission.Submit(accessionOrder.PEmailAddress, reportNo, accessionOrder.PFirstName);
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleTextAndEmailDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            Business.ReportDistribution.Model.DistributionResult distributionResult = Business.ReportDistribution.Model.TextAndEmailSubmission.Submit(accessionOrder.PPhoneNumberHome, accessionOrder.PEmailAddress, reportNo, accessionOrder.PFirstName);
            return distributionResult;
        }        

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleMTDOHDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            YellowstonePathology.Business.HL7View.CDC.MTDohResultView mtDohResultView = new Business.HL7View.CDC.MTDohResultView(reportNo, accessionOrder);
            mtDohResultView.CanSend(result);
            mtDohResultView.Send(result);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = result.Success;
            distributionResult.Message = result.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandlePrintDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.PrintDistribution printDistribution = new Business.ReportDistribution.Model.PrintDistribution();
            return printDistribution.Distribute(testOrderReportDistribution.ReportNo);            
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleWebServiceDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = new Business.ReportDistribution.Model.DistributionResult();
            result.IsComplete = true;
            return result;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleNoImplemented(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = false;
            distributionResult.Message = "Not Implemented";
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleATHENADistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            YellowstonePathology.Business.HL7View.CMMC.CMMCResultView cmmcResultView = new Business.HL7View.CMMC.CMMCResultView(reportNo, accessionOrder);
            YellowstonePathology.Business.Rules.MethodResult MmthodResult = new Business.Rules.MethodResult();
            cmmcResultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = methodResult.Success;
            distributionResult.Message = methodResult.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleMeditechDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("Meditech distribution is no longer valid.");
            //YellowstonePathology.Business.ReportDistribution.Model.MeditechDistribution meditechDistribution = new Business.ReportDistribution.Model.MeditechDistribution();            
            //return meditechDistribution.Distribute(reportNo, accessionOrder);
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleMeditechNMHDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.ReportDistribution.Model.MeditechNMHDistribution meditechNMHDistribution = new Business.ReportDistribution.Model.MeditechNMHDistribution();
            return meditechNMHDistribution.Distribute(reportNo, accessionOrder);
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleEMADistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.ReportDistribution.Model.EMADistribution emaDistribution = new Business.ReportDistribution.Model.EMADistribution();
            return emaDistribution.Distribute(reportNo, accessionOrder);
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleECWDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, Business.Test.AccessionOrder accessionOrder)
        {            
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();            
            YellowstonePathology.Business.HL7View.ECW.ECWResultView resultView = new Business.HL7View.ECW.ECWResultView(testOrderReportDistribution.ReportNo, accessionOrder, false);
            resultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = methodResult.Success;
            distributionResult.Message = methodResult.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleECWRiverstoneDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, Business.Test.AccessionOrder accessionOrder)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            Business.HL7View.Riverstone.RiverstoneResultView resultView = new Business.HL7View.Riverstone.RiverstoneResultView(testOrderReportDistribution.ReportNo, accessionOrder, false);
            resultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = methodResult.Success;
            distributionResult.Message = methodResult.Message;
            return distributionResult;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleFaxDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(testOrderReportDistribution.ReportNo);
            string pdfCaseFileName = Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);
            return YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(testOrderReportDistribution.FaxNumber, testOrderReportDistribution.ReportNo, pdfCaseFileName, testOrderReportDistribution.ReportNo);
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleEPICDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {                                        
            YellowstonePathology.Business.HL7View.IResultView resultView = Business.HL7View.ResultViewFactory.GetResultView(testOrderReportDistribution.ReportNo, accessionOrder, testOrderReportDistribution.ClientId, false, false);
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);                        

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = new Business.ReportDistribution.Model.DistributionResult();
            result.IsComplete = methodResult.Success;
            result.Message = methodResult.Message;
            return result;
        }        

        private void HandleWHP(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {            
            Business.PanelSet.Model.PanelSetCollection womensHealthTests = Business.PanelSet.Model.PanelSetCollection.GetWomensHealthTests();
            foreach(Business.PanelSet.Model.PanelSet ps in womensHealthTests)
            {
                if(accessionOrder.PanelSetOrderCollection.Exists(ps.PanelSetId))
                {
                    Business.Test.PanelSetOrder pso = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ps.PanelSetId);
                    pso.HoldDistribution = false;
                }                    
            }                           
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }           
    }
}