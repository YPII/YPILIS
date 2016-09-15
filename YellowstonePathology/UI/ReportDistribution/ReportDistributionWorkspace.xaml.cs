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
using StackExchange.Redis;

namespace YellowstonePathology.UI.ReportDistribution
{    
    public partial class ReportDistributionWorkspace : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        private System.Windows.Threading.DispatcherTimer m_Timer;
        private TimeSpan m_TimerIntervalFast = new TimeSpan(0, 1, 0);
        private TimeSpan m_TimerIntervalSlow = new TimeSpan(1, 0, 0);

        private DateTime m_PublishClock;
        private DateTime m_ScheduleClock;
        private YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntryCollection m_ReportDistributionLogEntryCollection;        
        private bool m_TimerRunning;        

        public ReportDistributionWorkspace()
        {            
            this.m_PublishClock = DateTime.Now;
            this.m_ScheduleClock = DateTime.Now;

            this.m_ReportDistributionLogEntryCollection = new Business.ReportDistribution.Model.ReportDistributionLogEntryCollection();            
            
            this.m_Timer = new System.Windows.Threading.DispatcherTimer();
            this.m_Timer.Interval = this.m_TimerIntervalFast;            
            this.m_Timer.Tick += new EventHandler(Timer_Tick);

            this.m_TimerRunning = true;

            InitializeComponent();

            this.DataContext = this;
            this.m_Timer.Start();

            this.SetStatus("Idle");
        }

        public bool TimmerRunning
        {
            get { return this.m_TimerRunning; }
            set { this.m_TimerRunning = value; }
        }

        public YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntryCollection ReportDistributionLogEntryCollection
        {
            get { return this.m_ReportDistributionLogEntryCollection; }
        }

        private void SetStatus(string status)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        this.TextBlockStatus.Text = status;
                    }));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.SetStatus("Processing");

            DateTime dailyStartTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 05:00");
            DateTime dailyEndTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 20:00");

            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish("ReportDistributionHeartBeat", "Hello");

            if (DateTime.Now >= dailyStartTime && DateTime.Now <= dailyEndTime)
            {                
                this.m_Timer.Stop();

                this.HandleUnscheduledAmendments();
                this.HandleUnsetDistribution(); 
                this.HandleUnscheduledPublish();
                this.HandleUnscheduledDistribution();
                this.PublishNext();

                this.SetStatus("Idle Office Hours. Next process starts: " + DateTime.Now.Add(this.m_TimerIntervalFast));
                this.m_Timer.Interval = this.m_TimerIntervalFast;
                this.m_Timer.Start();                
            }
            else
            {                
                this.m_Timer.Interval = this.m_TimerIntervalSlow;
                this.SetStatus("Idle After Hours");
            }
            
            this.m_Timer.Start();            
        }

        private void HandleUnscheduledAmendments()
        {            
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledAmendments();
            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
                foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
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
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCasesWithUnsetDistributions();

            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);				

                if(accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                    {
                        if (panelSetOrder.Final == true && panelSetOrder.Distribute == true)
                        {
                            if (panelSetOrder.TestOrderReportDistributionCollection.Count == 0)
                            {
                                YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(accessionOrder.PhysicianId, accessionOrder.ClientId);

                                if (physicianClientDistributionCollection.Count != 0)
                                {
                                    physicianClientDistributionCollection.SetDistribution(panelSetOrder, accessionOrder);
                                    this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unset Distribution", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                                        accessionOrder.PhysicianName, accessionOrder.ClientName, "Distribution Set");
                                }
                                else
                                {
                                    this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Handle Unset Distribution", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                                        accessionOrder.PhysicianName, accessionOrder.ClientName, "No Distribution Defined");

                                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Support@ypii.com", "Support@ypii.com", System.Windows.Forms.SystemInformation.UserName, "No Distribution Defined: " + panelSetOrder.ReportNo);
                                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

                                    Uri uri = new Uri("http://tempuri.org/");
                                    System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                                    System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                                    client.Credentials = credential;
                                    client.Send(message);
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
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledDistributions();
            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
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
            testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(15);

            panelSetOrder.Published = false;
            testOrderReportDistribution.Distributed = false;            
            panelSetOrder.ScheduledPublishTime = DateTime.Now.AddMinutes(15);                            

            this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Schedule Distribution", testOrderReportDistribution.DistributionType, panelSetOrder.ReportNo,
                panelSetOrder.MasterAccessionNo, testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "Distribution Scheduled");                                        
        }        

        private void HandleUnscheduledPublish()
        {
            List<YellowstonePathology.Business.MasterAccessionNo> caseList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCasesWithUnscheduledPublish();
            foreach (YellowstonePathology.Business.MasterAccessionNo masterAccessionNo in caseList)
            {                
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo.Value, this);
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

                            this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unschedule Publish", null, panelSetOrder.ReportNo,
                            panelSetOrder.MasterAccessionNo, null, null, "PanelSet Publish Sceduled");

                            if (panelSetOrder.Distribute == true)
                            {
                                foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                                {
                                    if (testOrderReportDistribution.Distributed == false && testOrderReportDistribution.ScheduledDistributionTime == null)
                                    {
                                        this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Handle Unschedule Publish", testOrderReportDistribution.DistributionType, panelSetOrder.ReportNo,
                                        panelSetOrder.MasterAccessionNo, testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "TestOrderReportDistribution Sceduled");

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
            caseDocument.Render();
            if(panelSetOrder.ResultDocumentSource != "Reference Lab")
            {
                caseDocument.Publish();
            }
                
            this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Publish Next", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo, null, null, "PanelSetOrder Published");

            //}                                    
            //catch (Exception publishException)
            //{
            //    this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo, null, null, publishException.Message);                
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
                this.DelayPublishAndDistribution(15, "Not able to delete files prior to publishing.", panelSetOrder);

                this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", null, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                                null, null, "Not able to delete files prior to publishing.");

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Support@ypii.com", "Support@ypii.com", System.Windows.Forms.SystemInformation.UserName, "Not able to delete files prior to publishing: " + panelSetOrder.ReportNo);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

                Uri uri = new Uri("http://tempuri.org/");
                System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                client.Credentials = credential;
                client.Send(message);

                result = false;
            }

            return result;
        }

        private void DelayPublishAndDistribution(int delayMinutes, string delayMessage, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            panelSetOrder.Published = false;
            panelSetOrder.TimeLastPublished = null;
            panelSetOrder.ScheduledPublishTime = DateTime.Now.AddMinutes(delayMinutes);            

            List<YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution> testOrderReportDistributionList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScheduledDistribution(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in testOrderReportDistributionList)
            {                
                testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(delayMinutes);
                testOrderReportDistribution.Rescheduled = true;
                testOrderReportDistribution.RescheduledMessage = delayMessage;                                
            }
        }

        private void PublishNext()
        {
            List<YellowstonePathology.Business.Test.PanelSetOrderView> caseList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNextCasesToPublish();            

            int maxProcessCount = 2;
            if (caseList.Count >= 10) maxProcessCount = 10;

            int processCount = 0;

            foreach (YellowstonePathology.Business.Test.PanelSetOrderView view in caseList)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(view.MasterAccessionNo, this);
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(view.ReportNo);

                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(accessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
                YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(panelSetOrder.ReportNo);
                
                if (panelSetOrder.HoldDistribution == false)
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

                                            this.m_ReportDistributionLogEntryCollection.AddEntry("INFO", "Publish Next", testOrderReportDistribution.DistributionType, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                                                testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, "TestOrderReportDistribution Distributed");
                                        }
                                        else
                                        {
                                            testOrderReportDistribution.ScheduledDistributionTime = DateTime.Now.AddMinutes(30);
                                            testOrderReportDistribution.Rescheduled = true;
                                            testOrderReportDistribution.RescheduledMessage = distributionResult.Message;

                                            this.m_ReportDistributionLogEntryCollection.AddEntry("ERROR", "Publish Next", testOrderReportDistribution.DistributionType, panelSetOrder.ReportNo, panelSetOrder.MasterAccessionNo,
                                                testOrderReportDistribution.PhysicianName, testOrderReportDistribution.ClientName, distributionResult.Message);

                                            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("Sid.Harder@ypii.com", "Sid.Harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, distributionResult.Message);
                                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

                                            Uri uri = new Uri("http://tempuri.org/");
                                            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                                            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                                            client.Credentials = credential;
                                            client.Send(message);
                                        }
                                    }
                                }
                            }

                            this.HandleNotificationEmail(panelSetOrder);

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

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void HandleNotificationEmail(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByMasterAccessionNo(panelSetOrder.MasterAccessionNo);
            if (physician.SendPublishNotifications == true)
            {
                if (panelSetOrder.Distribute == true)
                {                    
                    string subject = "You have a result ready for review: " + panelSetOrder.PanelSetName;
                    string body = "You have a patient report ready. You can review the report by using YPI Connect.  If you don't have access to YPI Connect please call us at (406)238-6360.";
                    
                    System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("Results@YPII.com");
                    System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(physician.PublishNotificationEmailAddress);
                    System.Net.Mail.MailAddress bcc = new System.Net.Mail.MailAddress("Results@YPII.com");

                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
                    message.Subject = subject;
                    message.Body = body;
                    message.Bcc.Add(bcc);
                    
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

                    Uri uri = new Uri("http://tempuri.org/");
                    System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                    System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                    client.Credentials = credential;
                    client.Send(message);

                    panelSetOrder.TimeOfLastPublishNotification = DateTime.Now;
                    panelSetOrder.PublishNotificationSent = true;                 
                }
            }
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult Distribute(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = null;            

            switch (testOrderReportDistribution.DistributionType)
            {
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX:
                    result = this.HandleFaxDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    result = this.HandleEPICDistribution(testOrderReportDistribution, accessionOrder, panelSetOrder);
                    break;                
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    result = this.HandleECWDistribution(testOrderReportDistribution, accessionOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    result = this.HandleATHENADistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    result = this.HandleMeditechDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE:
                    result = this.HandleWebServiceDistribution(testOrderReportDistribution);
                    break;                
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.PRINT:
                    result = this.HandlePrintDistribution(testOrderReportDistribution);
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MTDOH:
                    result = this.HandleMTDOHDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;            
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WYDOH:
                    result = this.HandleWYDOHDistribution(testOrderReportDistribution.ReportNo, accessionOrder);
                    break;
                default:
                    result = this.HandleNoImplemented(testOrderReportDistribution);
                    break;
            }

            return result;
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleWYDOHDistribution(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            YellowstonePathology.Business.HL7View.WYDOH.WYDOHResultView wyYDOHResultView = new Business.HL7View.WYDOH.WYDOHResultView(reportNo, accessionOrder);
            wyYDOHResultView.CanSend(result);
            wyYDOHResultView.Send(result);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult distributionResult = new Business.ReportDistribution.Model.DistributionResult();
            distributionResult.IsComplete = result.Success;
            distributionResult.Message = result.Message;
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
            YellowstonePathology.Business.ReportDistribution.Model.MeditechDistribution meditechDistribution = new Business.ReportDistribution.Model.MeditechDistribution();            
            return meditechDistribution.Distribute(reportNo, accessionOrder);
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

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleFaxDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(testOrderReportDistribution.ReportNo);
            string tifCaseFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
            return YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(testOrderReportDistribution.FaxNumber, testOrderReportDistribution.LongDistance, testOrderReportDistribution.ReportNo, tifCaseFileName);
        }

        private YellowstonePathology.Business.ReportDistribution.Model.DistributionResult HandleEPICDistribution(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution, Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
            if(panelSetOrder.IsPosted == true)
            {
                testOrderReportDistribution.ResultStatus = "F";
            }
            else
            {
                testOrderReportDistribution.ResultStatus = "P";
            }
                
            YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(testOrderReportDistribution.ReportNo, accessionOrder, testOrderReportDistribution.ClientId, testOrderReportDistribution.ResultStatus, false);
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);

            YellowstonePathology.Business.ReportDistribution.Model.DistributionResult result = new Business.ReportDistribution.Model.DistributionResult();
            result.IsComplete = methodResult.Success;
            result.Message = methodResult.Message;
            return result;
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void CheckBoxTimeRunning_Checked(object sender, RoutedEventArgs e)
        {
            this.m_Timer.Start();
        }

        private void CheckBoxTimerRunning_Unchecked(object sender, RoutedEventArgs e)
        {
            this.m_Timer.Stop();
        }     
    }
}