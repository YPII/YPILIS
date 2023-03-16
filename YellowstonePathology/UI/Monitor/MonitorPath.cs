using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using StackExchange.Redis;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.WebServices.Data;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Monitor
{
	public class MonitorPath
	{
        private static double TimerInterval = 1000 * 20;
        private static double AfterHoursTimerInterval = 1000 * 60 * 10;
        private static double InitialTimerInterval = 1000;

        private Queue<System.Windows.Controls.UserControl> m_PageQueue;
        private System.Timers.Timer m_Timer;
		private YellowstonePathology.UI.Monitor.MonitorPageWindow m_MonitorPageWindow;

        private DateTime m_LastReportDistributionHeartBeat;
        private DateTime m_LastBillingEODProcessRun;
        private bool m_DistributionDownTextHasBeenSent;

        public MonitorPath()
		{
            this.m_DistributionDownTextHasBeenSent = false;
            this.m_LastReportDistributionHeartBeat = DateTime.Now.AddMinutes(-5);
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Subscribe("ReportDistributionHeartBeat", (channel, message) =>
            {
                this.m_LastReportDistributionHeartBeat = DateTime.Now;
            });

            this.m_LastBillingEODProcessRun = DateTime.Today.AddDays(-1);
            
            this.m_PageQueue = new Queue<System.Windows.Controls.UserControl>();            
            this.m_MonitorPageWindow = new MonitorPageWindow();            
		}               

        public void Start()
        {                    	            
            this.StartTimer();                            
            this.m_MonitorPageWindow.Show();
        }                         

        public void Show(MonitorPageLoadEnum monitorPage)
        {
            switch (monitorPage)
            {
                case MonitorPageLoadEnum.CytologyScreeningMonitor:
                    CytologyScreeningMonitorPage cytologyScreeningMonitorPage = new CytologyScreeningMonitorPage();
                    cytologyScreeningMonitorPage.Refresh();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(cytologyScreeningMonitorPage);
                    break;
                case MonitorPageLoadEnum.ReportDistributionMonitor:
                    ReportDistributionMonitorPage reportDistributionMonitorPage = new ReportDistributionMonitorPage();
                    reportDistributionMonitorPage.Refresh();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(reportDistributionMonitorPage);
                    break;
                case MonitorPageLoadEnum.PendingTestMonitor:
                    PendingTestMonitorPage pendingTestMonitorPage = new PendingTestMonitorPage();
                    pendingTestMonitorPage.Refresh();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(pendingTestMonitorPage);
                    break;
                case MonitorPageLoadEnum.MissingInformationMonitor:
                    MissingInformationMonitorPage missingInformationMonitorPage = new MissingInformationMonitorPage();
                    missingInformationMonitorPage.Refresh();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(missingInformationMonitorPage);
                    break;
                case MonitorPageLoadEnum.DashboardMonitor:
                    DashboardPage dashboardPage = new DashboardPage();
                    dashboardPage.Refresh();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(dashboardPage);
                    break;
                case MonitorPageLoadEnum.BillingEODProcess:
                    BillingEODProcessingPage billingEODProcessingPage = new BillingEODProcessingPage();
                    billingEODProcessingPage.Refresh();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(billingEODProcessingPage);
                    break;
                case MonitorPageLoadEnum.HuddleDashboard:
                    HuddleDashboardPage huddleDashboardPage = new HuddleDashboardPage();
                    this.m_MonitorPageWindow.PageNavigator.Navigate(huddleDashboardPage);
                    break;
            }
            this.m_MonitorPageWindow.Show();
        }

        public void LoadAllPages()
        {            
            CytologyScreeningMonitorPage cytologyScreeningMonitorPage = new CytologyScreeningMonitorPage();            
            this.m_PageQueue.Enqueue(cytologyScreeningMonitorPage);

            ReportDistributionMonitorPage reportDistributionMonitorPage = new ReportDistributionMonitorPage();            
            this.m_PageQueue.Enqueue(reportDistributionMonitorPage);

            PendingTestMonitorPage pendingTestMonitorPage = new PendingTestMonitorPage();
            this.m_PageQueue.Enqueue(pendingTestMonitorPage);

            MissingInformationMonitorPage missingInformationPage = new MissingInformationMonitorPage();
            this.m_PageQueue.Enqueue(missingInformationPage);                     

            DashboardPage dashboardPage = new Monitor.DashboardPage();
            this.m_PageQueue.Enqueue(dashboardPage);
        }

        public void StartTimer()
        {
            this.m_Timer = new System.Timers.Timer();
            this.m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);            
            this.m_Timer.Interval = InitialTimerInterval;            
            this.m_Timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {            
            this.m_Timer.Stop();
            
            DateTime timerDailyStartTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 04:00");
            DateTime timerDailyEndTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 22:00");
            
            this.m_MonitorPageWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new System.Action(
                    delegate()
                    {
                        if (DateTime.Now >= timerDailyStartTime && DateTime.Now <= timerDailyEndTime)
                        {
                        	this.m_Timer.Interval = TimerInterval;
                        	if(this.UnreadAutopsyRequestExist() == true)
                        	{
                                this.ShowUnhandledAutopsyRequestPage();                               
                        	}
                            else if(this.m_LastReportDistributionHeartBeat < DateTime.Now.AddMinutes(-15))
                            {
                                this.ShowReportDistributionDownPage();
                            }
                        	else
                        	{
                                if(this.m_DistributionDownTextHasBeenSent == true)
                                {
                                    JObject jsonRequest = Business.APIRequestHelper.GetSendTextMessage("4068603984", "YPI Report Distribution is back up.");
                                    Business.APIResult apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);

                                    jsonRequest = Business.APIRequestHelper.GetSendTextMessage("4066975699", "YPI Report Distribution is back up.");
                                    apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
                                    
                                    this.m_DistributionDownTextHasBeenSent = false;
                                }
                                
                                this.ShowNextPage();
                            }
                        }
                        else
                        {
                            //if (this.ShowBillingEODProcessingPage() == false)
                            //{
                                this.m_Timer.Interval = AfterHoursTimerInterval;
                                this.m_Timer.Start();

                                GoodNightPage goodNightPage = new GoodNightPage();
                                this.m_MonitorPageWindow.PageNavigator.Navigate(goodNightPage);
                            //}
                        }

                    })); 
        }        

        private void ShowNextPage()
        {
            if (this.m_PageQueue.Count > 0)
            {                
                System.Windows.Controls.UserControl userControl = this.m_PageQueue.Dequeue();
                IMonitorPage monitorPage = (IMonitorPage)userControl;
                monitorPage.Refresh();
                this.m_MonitorPageWindow.PageNavigator.Navigate(userControl);
                this.m_PageQueue.Enqueue(userControl);
                this.m_Timer.Start();
            }            
        }
               
        private bool UnreadAutopsyRequestExist()
        {                  
        	bool result = false;            
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service.Credentials = new WebCredentials("ypiilab\\histology", "Let'sMakeSlides");            

            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;

            service.AutodiscoverUrl("sid.harder@ypii.com", RedirectionUrlValidationCallback);
            SearchFilter searchFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);

            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            searchFilterCollection.Add(searchFilter);
            ItemView view = new ItemView(50);
            view.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeReceived);
            view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            view.Traversal = ItemTraversal.Shallow;
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, searchFilter, view);
            if (findResults.Items.Count > 0) result = true;                
            return result;
        }         
        
        private void ShowUnhandledAutopsyRequestPage()
        {            
            AutopsyRequestMonitorPage autopsyRequestMonitorPage = new AutopsyRequestMonitorPage();
        	this.m_MonitorPageWindow.PageNavigator.Navigate(autopsyRequestMonitorPage);
            this.m_Timer.Start();
        }

        private void ShowReportDistributionDownPage()
        {
            if(this.m_DistributionDownTextHasBeenSent == false)
            {
                JObject jsonRequest = Business.APIRequestHelper.GetSendTextMessage("4068603984", "YPI Report Distribution is down.");
                Business.APIResult apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);

                jsonRequest = Business.APIRequestHelper.GetSendTextMessage("4066975699", "YPI Report Distribution is down.");
                apiResult = Business.APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
                this.m_DistributionDownTextHasBeenSent = true;
            }            

            ReportDistributionDownMonitorPage reportDistributionDownMonitorPage = new ReportDistributionDownMonitorPage();
            this.m_MonitorPageWindow.PageNavigator.Navigate(reportDistributionDownMonitorPage);
            this.m_Timer.Start();
        }

        private bool ShowBillingEODProcessingPage()
        {
            bool result = false;
            DateTime billingEODWidowOpen = DateTime.Parse(DateTime.Today.ToShortDateString() + " 20:00");
            DateTime billingEODWidowClose = DateTime.Parse(DateTime.Today.ToShortDateString() + " 21:01");
            if (DateTime.Now >= billingEODWidowOpen && DateTime.Now <= billingEODWidowClose && this.m_LastBillingEODProcessRun < DateTime.Today)
            {
                result = true;
                this.m_LastBillingEODProcessRun = DateTime.Today;
                BillingEODProcessingPage billingEODProcessingPage = new Monitor.BillingEODProcessingPage();
                this.m_MonitorPageWindow.PageNavigator.Navigate(billingEODProcessingPage);
                billingEODProcessingPage.Start();
                this.m_Timer.Start();
            }

            return result;
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        private static bool CertificateValidationCallBack(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }
    }
}
