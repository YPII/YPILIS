using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using StackExchange.Redis;

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

		private Microsoft.Office.Interop.Outlook.Application m_OutlookApp;
        private Microsoft.Office.Interop.Outlook._NameSpace m_OutlookNameSpace;
        private Microsoft.Office.Interop.Outlook.MAPIFolder m_MAPIFolder;
        private Microsoft.Office.Interop.Outlook._Explorer m_Explorer;

        private DateTime m_LastReportDistributionHeartBeat;

        public MonitorPath()
		{        				
        	this.m_OutlookApp = new Microsoft.Office.Interop.Outlook.Application();
            this.m_OutlookNameSpace = (Microsoft.Office.Interop.Outlook._NameSpace)this.m_OutlookApp.GetNamespace("MAPI");
            this.m_MAPIFolder = this.m_OutlookNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
            this.m_Explorer = this.m_MAPIFolder.GetExplorer(false);
            this.m_OutlookNameSpace.Logon(System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, true);

            this.m_LastReportDistributionHeartBeat = DateTime.Now.AddMinutes(-5);

            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Subscribe("ReportDistributionHeartBeat", (channel, message) =>
            {
                this.m_LastReportDistributionHeartBeat = DateTime.Now;
            });

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
            
            DateTime timerDailyStartTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 05:00");
            DateTime timerDailyEndTime = DateTime.Parse(DateTime.Today.ToShortDateString() + " 18:00");
            
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
                                this.ShowNextPage();
                            }
                        }
                        else
                        {
                        	this.m_Timer.Interval = AfterHoursTimerInterval;
                        	this.m_Timer.Start();
                        	
                            GoodNightPage goodNightPage = new GoodNightPage();
                            this.m_MonitorPageWindow.PageNavigator.Navigate(goodNightPage);
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
            			     	
            Microsoft.Office.Interop.Outlook.Items items = this.m_MAPIFolder.Items;
            foreach (object item in items)
            {
                if(item is Microsoft.Office.Interop.Outlook.MailItem)
                {
                    Microsoft.Office.Interop.Outlook.MailItem mailItem = (Microsoft.Office.Interop.Outlook.MailItem)item;
                    if (mailItem.UnRead)
                    {
                        result = true;
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(item);
                        break;
                    }
                }                
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(item);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(items);            			
			
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
            ReportDistributionDownMonitorPage reportDistributionDownMonitorPage = new ReportDistributionDownMonitorPage();
            this.m_MonitorPageWindow.PageNavigator.Navigate(reportDistributionDownMonitorPage);
            this.m_Timer.Start();
        }
    }
}
