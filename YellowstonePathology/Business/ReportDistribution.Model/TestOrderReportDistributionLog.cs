using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml.Linq;
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

        public TestOrderReportDistributionLog()
        {

        }

		public TestOrderReportDistributionLog(string testOrderReportDistributionLogId, string objectId)
		{
			this.m_TestOrderReportDistributionLogId = testOrderReportDistributionLogId;
			this.m_ObjectId = objectId;
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
