using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class ReportDistributionLogEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ObjectId;
        private DateTime m_Date;
        private string m_Level;        
        private string m_Source;
        private string m_ReportNo;
        private string m_MasterAccessionNo;
        private string m_ClientName;
        private string m_PhysicianName;
        private string m_DistributionType;
        private string m_Message;

        public ReportDistributionLogEntry()
        {

        }

		public ReportDistributionLogEntry(string objectId)
		{
			this.m_ObjectId = objectId;
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

        [PersistentProperty()]
        public DateTime Date
        {
            get { return this.m_Date; }
            set
            {
                if (this.m_Date != value)
                {
                    this.m_Date = value;
                    this.NotifyPropertyChanged("Date");
                }
            }
        }

        [PersistentProperty()]
        public string Level
        {
            get {return this.m_Level;}
            set
            {
                if (this.m_Level != value)
                {
                    this.m_Level = value;
                    this.NotifyPropertyChanged("Level");
                }
            }
        }

        [PersistentProperty()]
        public string Source
        {
            get { return this.m_Source; }
            set
            {
                if (this.m_Source != value)
                {
                    this.m_Source = value;
                    this.NotifyPropertyChanged("Source");
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
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
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
        public string Message
        {
            get { return this.m_Message; }
            set
            {
                if (this.m_Message != value)
                {
                    this.m_Message = value;
                    this.NotifyPropertyChanged("m_Message");
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
