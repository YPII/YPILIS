using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Monitor.Model
{
	public class CytologyScreening : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_AccessionTime;        
        private string m_ReportNo;
        private string m_ScreeningType;
        private string m_ScreenedByName;
        private string m_AssignedToName;
        private Nullable<DateTime> m_ExpectedFinalTime;
        private string m_ClientName;
        private string m_ProviderName;
        private MonitorStateEnum m_State;
        private TimeSpan m_RunningTime;
        private string m_RunningTimeString;
        private TimeSpan m_GoalTime;
        private string m_GoalTimeString;
        private TimeSpan m_Difference;
        private string m_DifferenceString;
        private int m_ScreeningCount;
        private bool m_IsDelayed;

        public CytologyScreening()
        {

		}        

        [PersistentProperty()]
        public DateTime AccessionTime
        {
            get { return this.m_AccessionTime; }
            set
            {
                if (this.m_AccessionTime != value)
                {
                    this.m_AccessionTime = value;
                    this.NotifyPropertyChanged("AccessionTime");
                }
            }
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
        public string ScreeningType
        {
            get { return this.m_ScreeningType; }
            set
            {
                if (this.m_ScreeningType != value)
                {
                    this.m_ScreeningType = value;
                    this.NotifyPropertyChanged("ScreeningType");
                }
            }
        }

        [PersistentProperty()]
        public string ScreenedByName
        {
            get { return this.m_ScreenedByName; }
            set
            {
                if (this.m_ScreenedByName != value)
                {
                    this.m_ScreenedByName = value;
                    this.NotifyPropertyChanged("ScreenedByName");
                }
            }
        }

        [PersistentProperty()]
        public string AssignedToName
        {
            get { return this.m_AssignedToName; }
            set
            {
                if (this.m_AssignedToName != value)
                {
                    this.m_AssignedToName = value;
                    this.NotifyPropertyChanged("AssignedToName");
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
        public int ScreeningCount
        {
            get { return this.m_ScreeningCount; }
            set
            {
                if (this.m_ScreeningCount != value)
                {
                    this.m_ScreeningCount = value;
                    this.NotifyPropertyChanged("ScreeningCount");
                }
            }
        }

        [PersistentProperty()]
        public bool IsDelayed
        {
            get { return this.m_IsDelayed; }
            set
            {
                if (this.m_IsDelayed != value)
                {
                    this.m_IsDelayed = value;
                    this.NotifyPropertyChanged("IsDelayed");
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

        public void SetState(YellowstonePathology.Business.Calendar.HolidayCollection holidays)
        {
            this.SetRunningTime(holidays);
            this.SetGoalTime(holidays);
            this.SetDifference();

            if (this.m_Difference.TotalMinutes < 0)
            {
                this.m_State = MonitorStateEnum.Critical;
            }
            else if (this.m_IsDelayed == true)
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

        private void SetGoalTime(Calendar.HolidayCollection holidays)
        {
            this.m_GoalTime = Business.Helper.DateTimeExtensions.GetHoursBetween(this.m_AccessionTime, this.m_ExpectedFinalTime.Value, holidays);

            if (this.m_GoalTime.TotalHours <= 48)
            {
                this.m_GoalTimeString = Math.Round(this.m_GoalTime.TotalHours, 0).ToString() + " hrs";

            }
            else
            {
                this.m_GoalTimeString = Math.Round(this.m_GoalTime.TotalDays, 0).ToString() + " days";
            }
        }

        private void SetRunningTime(Calendar.HolidayCollection holidays)
        {
            this.m_RunningTime = Business.Helper.DateTimeExtensions.GetHoursBetween(this.m_AccessionTime, DateTime.Now, holidays);
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
            get { return this.m_Difference; }
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
