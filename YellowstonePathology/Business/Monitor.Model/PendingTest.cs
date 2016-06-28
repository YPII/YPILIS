using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Monitor.Model
{
	public class PendingTest : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;
        private string m_TestName;
        private DateTime m_OrderTime;                
        private Nullable<DateTime> m_ExpectedFinalTime;
        private string m_AssignedTo;
        private string m_ClientName;
        private string m_ProviderName;
        private MonitorStateEnum m_State;
        private TimeSpan m_RunningTime;
        private string m_RunningTimeString;
        private TimeSpan m_GoalTime;
        private string m_GoalTimeString;
        private TimeSpan m_Difference;
        private string m_DifferenceString;
        private bool m_Delayed;

        public PendingTest()
        {

		}

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        public string TestName
        {
            get { return this.m_TestName; }
            set
            {
                if (this.m_TestName != value)
                {
                    this.m_TestName = value;
                    this.NotifyPropertyChanged("TestName");
                }
            }
        }

        [PersistentProperty()]
        public DateTime OrderTime
        {
            get { return this.m_OrderTime; }
            set
            {
                if (this.m_OrderTime != value)
                {
                    this.m_OrderTime = value;
                    this.NotifyPropertyChanged("OrderTime");
                }
            }
        }                

        [PersistentProperty()]
        public Nullable<DateTime> ExpectedFinalTime
        {
            get { return this.m_ExpectedFinalTime; }
            set
            {
                if (this.m_ExpectedFinalTime != value)
                {
                    this.m_ExpectedFinalTime = value;
                    this.NotifyPropertyChanged("ExpectedFinalTime");
                }
            }
        }    

        [PersistentProperty()]
        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set
            {
                if (this.m_AssignedTo != value)
                {
                    this.m_AssignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }           

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }

        [PersistentProperty()]
        public string ProviderName
        {
            get { return this.m_ProviderName; }
            set
            {
                if (this.m_ProviderName != value)
                {
                    this.m_ProviderName = value;
                    this.NotifyPropertyChanged("ProviderName");
                }
            }
        }

        [PersistentProperty()]
        public bool Delayed
        {
            get { return this.m_Delayed; }
            set
            {
                if (this.m_Delayed != value)
                {
                    this.m_Delayed = value;
                    this.NotifyPropertyChanged("Delayed");
                }
            }
        }        

        public MonitorStateEnum State
        {
            get { return this.m_State; }
            set
            {
                if (this.m_State != value)
                {
                    this.m_State = value;
                    this.NotifyPropertyChanged("State");
                }
            }
        }

        public void SetState()
        {
            this.SetRunningTime();
            this.SetGoalTime();
            this.SetDifference();

            if (this.m_Difference.TotalMinutes < 0)
            {
                this.m_State = MonitorStateEnum.Critical;
            }
            else if (this.m_Delayed == true)
            {
                this.m_State = MonitorStateEnum.Warning;
            }
            else
            {
                this.m_State = MonitorStateEnum.Normal;
            }
        }

        private void SetDifference()
        {
            this.m_Difference = this.m_GoalTime - this.m_RunningTime;
            if (this.m_Difference.TotalHours >= -48 && this.m_Difference.TotalHours <= 48)
            {
                this.m_DifferenceString = Math.Round(this.m_Difference.TotalHours, 0).ToString() + " hrs";
            }            
            else
            {
                this.m_DifferenceString = Math.Round(this.m_Difference.TotalDays, 0).ToString() + " days";
            }
        }

        private void SetGoalTime()
        {
            this.m_GoalTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetHoursBetweenExcludingWeekends(this.m_OrderTime, this.m_ExpectedFinalTime.Value);
            
            if (this.m_GoalTime.TotalHours <= 48)
            {
                this.m_GoalTimeString = Math.Round(this.m_GoalTime.TotalHours, 0).ToString() + " hrs";

            }
            else
            {
                this.m_GoalTimeString = Math.Round(this.m_GoalTime.TotalDays, 0).ToString() + " days";
            }
        }

        private void SetRunningTime()
        {
            this.m_RunningTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetHoursBetweenExcludingWeekends(this.m_OrderTime, DateTime.Now);
            if (this.m_RunningTime.TotalHours <= 48)
            {
                this.m_RunningTimeString = Math.Round(this.m_RunningTime.TotalHours, 0).ToString() + " hrs";

            }
            else
            {
                this.m_RunningTimeString = Math.Round(this.m_RunningTime.TotalDays, 0).ToString() + " days";
            }
        }

        public TimeSpan RunningTime
        {
            get { return this.m_RunningTime; }
        }

        public string RunningTimeString
        {
            get { return this.m_RunningTimeString; }
        }

        public TimeSpan GoalTime
        {
            get { return this.m_GoalTime; }
        }

        public string GoalTimeString
        {
            get { return this.m_GoalTimeString; }
        }

        public TimeSpan Difference
        {
            get { return this.m_Difference;}
        }

        public string DifferenceString
        {
            get { return this.m_DifferenceString; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
	}
}
