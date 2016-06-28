using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;
using System.Xml.Linq;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
	[PersistentClass("tblTestOrderReportDistribution", "YPIDATA")]
	public class TestOrderReportDistribution : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_TestOrderReportDistributionId;        
		private string m_ReportNo;
		private int m_PhysicianId;
		private int m_ClientId;
		private string m_PhysicianName;
		private string m_ClientName;
		private string m_DistributionType;
        private bool m_Distributed;
        private Nullable<DateTime> m_DateAdded;
        private Nullable<DateTime> m_TimeOfLastDistribution;
        private Nullable<DateTime> m_ScheduledDistributionTime;
        private string m_FaxNumber;
        private bool m_LongDistance;
        private bool m_Rescheduled;
        private string m_RescheduledMessage;        

		public TestOrderReportDistribution()
        {

        }

        public TestOrderReportDistribution(string testOrderReportDistributionId, string objectId, string reportNo, int physicianId, string physicianName, int clientId, 
            string clientName, string distributionType, string faxNumber, bool longDistance)
		{
            this.m_TestOrderReportDistributionId = testOrderReportDistributionId;
			this.m_ObjectId = objectId;
			this.m_ReportNo = reportNo;
			this.m_PhysicianId = physicianId;
            this.m_PhysicianName = physicianName;
			this.m_ClientId = clientId;			
			this.m_ClientName = clientName;
            this.m_DistributionType = distributionType;
            this.m_DateAdded = DateTime.Now;
            this.m_Distributed = false;
            this.m_ScheduledDistributionTime = null;
            this.m_FaxNumber = faxNumber;
            this.m_LongDistance = longDistance;
		}

        [PersistentDocumentIdProperty()]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
        }

		[PersistentPrimaryKeyProperty(false)]
        public string TestOrderReportDistributionId
		{
            get { return this.m_TestOrderReportDistributionId; }
			set
			{
                if (this.m_TestOrderReportDistributionId != value)
				{
                    this.m_TestOrderReportDistributionId = value;
                    this.NotifyPropertyChanged("TestOrderReportDistributionId");
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
		public int PhysicianId
		{
			get { return this.m_PhysicianId; }
			set
			{
				if (this.m_PhysicianId != value)
				{
					this.m_PhysicianId = value;
					this.NotifyPropertyChanged("PhysicianId");
				}
			}
		}

		[PersistentProperty()]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
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
		public string DistributionType
		{
			get { return this.m_DistributionType; }
			set
			{
				if (this.m_DistributionType != value)
				{
					this.m_DistributionType = value;
					this.NotifyPropertyChanged("DistributionType");
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
        public Nullable<DateTime> DateAdded
        {
            get { return this.m_DateAdded; }
            set
            {
                if (this.m_DateAdded != value)
                {
                    this.m_DateAdded = value;
                    this.NotifyPropertyChanged("DateAdded");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> ScheduledDistributionTime
        {
            get { return this.m_ScheduledDistributionTime; }
            set
            {
                if (this.m_ScheduledDistributionTime != value)
                {
                    this.m_ScheduledDistributionTime = value;
                    this.NotifyPropertyChanged("ScheduledDistributionTime");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> TimeOfLastDistribution
        {
            get { return this.m_TimeOfLastDistribution; }
            set
            {
                if (this.m_TimeOfLastDistribution != value)
                {
                    this.m_TimeOfLastDistribution = value;
                    this.NotifyPropertyChanged("TimeOfLastDistribution");
                }
            }
        }

        [PersistentProperty()]
        public string FaxNumber
        {
            get { return this.m_FaxNumber; }
            set
            {
                if (this.m_FaxNumber != value)
                {
                    this.m_FaxNumber = value;
                    this.NotifyPropertyChanged("FaxNumber");
                }
            }
        }

        [PersistentProperty()]
        public bool LongDistance
        {
            get { return this.m_LongDistance; }
            set
            {
                if (this.m_LongDistance != value)
                {
                    this.m_LongDistance = value;
                    this.NotifyPropertyChanged("LongDistance");
                }
            }
        }

        [PersistentProperty()]
        public bool Rescheduled
        {
            get { return this.m_Rescheduled; }
            set
            {
                if (this.m_Rescheduled != value)
                {
                    this.m_Rescheduled = value;
                    this.NotifyPropertyChanged("Rescheduled");
                }
            }
        }

        [PersistentProperty()]
        public string RescheduledMessage
        {
            get { return this.m_RescheduledMessage; }
            set
            {
                if (this.m_RescheduledMessage != value)
                {
                    this.m_RescheduledMessage = value;
                    this.NotifyPropertyChanged("RescheduledMessage");
                }
            }
        }

        public void ScheduleForDistribution(Nullable<DateTime> timeToSchedule)
        {
            this.m_Distributed = false;
            this.m_ScheduledDistributionTime = timeToSchedule;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void UnScheduleForDistribution()
        {
            if (this.m_TimeOfLastDistribution.HasValue == true)
            {
                this.m_Distributed = true;
            }
            this.m_ScheduledDistributionTime = null;
            this.NotifyPropertyChanged(string.Empty);
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
				
