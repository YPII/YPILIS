using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
	[PersistentClass("tblTestOrderReportDistributionLog", "YPIDATA")]
	public class TestOrderReportDistributionLog : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_TestOrderReportDistributionLogId;		
		private string m_ReportNo;
		private int m_PhysicianId;
		private int m_ClientId;
		private string m_PhysicianName;
		private string m_ClientName;
		private string m_DistributionType;		
		private bool m_ErrorInDistribution;
        private string m_Comment;
        private Nullable<DateTime> m_TimeDistributed;

        // these 2 properties added to allow ws_ stored procedures to work
        private bool m_CaseDistributed;
        private DateTime? m_DateDistributed;

        public TestOrderReportDistributionLog()
        {

        }

		public TestOrderReportDistributionLog(string testOrderReportDistributionLogId, string objectId)
		{
			this.m_TestOrderReportDistributionLogId = testOrderReportDistributionLogId;
			this.m_ObjectId = objectId;
            this.m_CaseDistributed = false;
		}

        public void FromTestOrderReportDistribution(TestOrderReportDistribution testOrderReportDistribution)
        {
            this.m_ReportNo = testOrderReportDistribution.ReportNo;
            this.m_PhysicianId = testOrderReportDistribution.PhysicianId;
            this.m_PhysicianName = testOrderReportDistribution.PhysicianName;
            this.m_ClientId = testOrderReportDistribution.ClientId;
            this.m_ClientName = testOrderReportDistribution.ClientName;
            this.m_DistributionType = testOrderReportDistribution.DistributionType;
        }

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "100", "null", "varchar")]
		public string TestOrderReportDistributionLogId
		{
            get { return this.m_TestOrderReportDistributionLogId; }
			set
			{
                if (this.m_TestOrderReportDistributionLogId != value)
				{
                    this.m_TestOrderReportDistributionLogId = value;
                    this.NotifyPropertyChanged("TestOrderReportDistributionLogId");
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> TimeDistributed
		{
			get { return this.m_TimeDistributed; }
			set
			{
				if (this.m_TimeDistributed != value)
				{
					this.m_TimeDistributed = value;
					this.NotifyPropertyChanged("TimeDistributed");
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
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
		[PersistentDataColumnProperty(true, "11", "null", "int")]
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
		[PersistentDataColumnProperty(true, "250", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "250", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool ErrorInDistribution
		{
			get { return this.m_ErrorInDistribution; }
			set
			{
				if (this.m_ErrorInDistribution != value)
				{
					this.m_ErrorInDistribution = value;
					this.NotifyPropertyChanged("ErrorInDistribution");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "'0'", "tinyint")]
        public bool CaseDistributed
        {
            get { return this.m_CaseDistributed; }
            set
            {
                if (this.m_CaseDistributed != value)
                {
                    this.m_CaseDistributed = value;
                    this.NotifyPropertyChanged("CaseDistributed");
                }
            }
        }


        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public DateTime? DateDistributed
        {
            get { return this.m_DateDistributed; }
            set
            {
                if (this.m_DateDistributed != value)
                {
                    this.m_DateDistributed = value;
                    this.NotifyPropertyChanged("DateDistributed");
                }
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
