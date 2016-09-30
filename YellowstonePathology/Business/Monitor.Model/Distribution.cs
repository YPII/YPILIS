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
	public class Distribution : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        static int MinutesSinceScheduledThreshold = 30;        

        private DateTime m_AccessionTime;
        private string m_ReportNo;
        private string m_PanelSetName;
        private Nullable<DateTime> m_FinalTime;
        private string m_PhysicianName;
        private string m_ClientName;
        private bool m_Distributed;
        private Nullable<int> m_MinutesSinceScheduled;        
        private string m_State;

        public Distribution()
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
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> FinalTime
        {
            get { return this.m_FinalTime; }
            set
            {
                if (this.m_FinalTime != value)
                {
                    this.m_FinalTime = value;
                    this.NotifyPropertyChanged("FinalTime");
                }
            }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set
            {
                if (this.m_PhysicianName != value)
                {
                    this.m_PhysicianName = value;
                    this.NotifyPropertyChanged("PhysicianName");
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
        public bool Distributed
        {
            get { return this.m_Distributed; }
            set
            {
                if (this.m_Distributed != value)
                {
                    this.m_Distributed = value;
                    this.NotifyPropertyChanged("Distributed");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<int> MinutesSinceScheduled
        {
            get { return this.m_MinutesSinceScheduled; }
            set
            {
                if (this.m_MinutesSinceScheduled != value)
                {
                    this.m_MinutesSinceScheduled = value;
                    this.NotifyPropertyChanged("MinutesSinceScheduled");
                }
            }
        }

        public string State
        {
            get { return this.m_State; }
            set
            {
                if (this.m_State != value)
                {
                    this.m_State = value;
                    this.NotifyPropertyChanged("m_State");
                }
            }
        }

        public void SetState()
        {
            if (this.Distributed == true)
            {
                this.m_State = "Normal";
            }            
            else if (this.m_MinutesSinceScheduled < MinutesSinceScheduledThreshold)
            {
                this.m_State = "Warning";
            }
            else
            {
                this.m_State = "Critical";
            }
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
