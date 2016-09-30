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
	public class CytologyScreening : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_AccessionTime;        
        private string m_ReportNo;
        private bool m_Final;
        private string m_ScreeningType;
        private string m_ScreenedByName;
        private string m_AssignedToName;
        private Nullable<DateTime> m_ScreeningFinalTime;
        private Nullable<DateTime> m_CaseFinalTime;
        private string m_ClientName;
        private string m_ProviderName;
        private MonitorStateEnum m_State;
        private TimeSpan m_HoursSinceAccessioned;
        private int m_ScreeningCount;

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
        public bool Final
        {
            get { return this.m_Final; }
            set
            {
                if (this.m_Final != value)
                {
                    this.m_Final = value;
                    this.NotifyPropertyChanged("Final");
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
        public Nullable<DateTime> ScreeningFinalTime
        {
            get { return this.m_ScreeningFinalTime; }
            set
            {
                if (this.m_ScreeningFinalTime != value)
                {
                    this.m_ScreeningFinalTime = value;
                    this.NotifyPropertyChanged("ScreeningFinalTime");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> CaseFinalTime
        {
            get { return this.m_CaseFinalTime; }
            set
            {
                if (this.m_CaseFinalTime != value)
                {
                    this.m_CaseFinalTime = value;
                    this.NotifyPropertyChanged("CaseFinalTime");
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
            TimeSpan singleScreenLookback = new TimeSpan(24, 0, 0);
            TimeSpan multipleScreenLookback = new TimeSpan(30, 0, 0);

            this.m_HoursSinceAccessioned = YellowstonePathology.Business.Helper.DateTimeExtensions.GetHoursBetweenExcludingWeekends(this.m_AccessionTime, DateTime.Now);                    

            if (this.Final == false)
            {
                if (this.m_AccessionTime.Day == DateTime.Today.Day)
                {
                    this.m_State = MonitorStateEnum.Normal;                 
                }
                else if (this.m_AccessionTime.Day != DateTime.Today.Day)
                {                    
                    if (this.m_ScreeningCount == 1)
                    {
                        if (this.m_HoursSinceAccessioned.TotalHours > singleScreenLookback.TotalHours)
                        {
                            this.m_State = MonitorStateEnum.Critical;
                        }
                        else
                        {
                            this.m_State = MonitorStateEnum.Warning;
                        }
                    }
                    else
                    {
                        if (this.m_HoursSinceAccessioned.TotalHours > multipleScreenLookback.TotalHours)
                        {
                            this.m_State = MonitorStateEnum.Critical;
                        }
                        else
                        {
                            this.m_State = MonitorStateEnum.Warning;
                        }
                    }                    
                }
            }
            else
            {
                this.m_State = MonitorStateEnum.Normal;
            }
        }

        public TimeSpan HoursSinceAccessioned
        {
            get { return this.m_HoursSinceAccessioned; }
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
