using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Search
{
	public class PathologistSearchResult : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		bool m_Assign;

        private string m_MasterAccessionNo;
        private string m_ReportNo;
        private DateTime m_AccessionDate;
        private Nullable<DateTime> m_FinalDate;
        private bool m_Final;
        private Nullable<DateTime> m_AcceptedTime;
        private bool m_Accepted;
        private int m_PanelSetId;        
        private string m_PatientName;
        private Nullable<DateTime> m_PBirthdate;
        private string m_PatientId;
        private string m_PathologistName;
        private string m_AcceptedBy;
        private string m_GroupType;
		private string m_TestName;
        private DateTime m_ExpectedFinalTime;

		public PathologistSearchResult()
		{
			m_Assign = false;
		}

		public bool Assign
		{
			get { return this.m_Assign; }
			set { this.m_Assign = value; }
		}

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
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

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
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

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate; }
            set
            {
                if (this.m_AccessionDate != value)
                {
                    this.m_AccessionDate = value;
                    this.NotifyPropertyChanged("AccessionDate");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public Nullable<DateTime> FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;
                    this.NotifyPropertyChanged("FinalDate");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
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

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public Nullable<DateTime> AcceptedTime
        {
			get { return this.m_AcceptedTime; }
            set
            {
				if (this.m_AcceptedTime != value)
                {
					this.m_AcceptedTime = value;
					this.NotifyPropertyChanged("AcceptedTime");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public bool Accepted
        {
            get { return this.m_Accepted; }
            set
            {
                if (this.m_Accepted != value)
                {
                    this.m_Accepted = value;
                    this.NotifyPropertyChanged("Accepted");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;
                    this.NotifyPropertyChanged("PanelSetId");
                }
            }
        }        

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public string PatientName
        {
            get { return this.m_PatientName; }
            set
            {
                if (this.m_PatientName != value)
                {
                    this.m_PatientName = value;
                    this.NotifyPropertyChanged("PatientName");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set
            {
                if (this.m_PBirthdate != value)
                {
                    this.m_PBirthdate = value;
                    this.NotifyPropertyChanged("PBirthdate");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public string PatientId
        {
            get { return this.m_PatientId; }
            set
            {
                if (this.m_PatientId != value)
                {
                    this.m_PatientId = value;
                    this.NotifyPropertyChanged("PatientId");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public string PathologistName
        {
            get { return this.m_PathologistName; }
            set
            {
                if (this.m_PathologistName != value)
                {
                    this.m_PathologistName = value;
                    this.NotifyPropertyChanged("PathologistName");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
		public string AcceptedBy
        {
			get { return this.m_AcceptedBy; }
            set
            {
				if (this.m_AcceptedBy != value)
                {
					this.m_AcceptedBy = value;
					this.NotifyPropertyChanged("AcceptedBy");
                }
            }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public string GroupType
        {
            get { return this.m_GroupType; }
            set
            {
                if (this.m_GroupType != value)
                {
                    this.m_GroupType = value;
                    this.NotifyPropertyChanged("GroupType");
                }
            }
        }

		[YellowstonePathology.Business.Persistence.PersistentProperty()]
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

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public DateTime ExpectedFinalTime
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

        public bool IsInCriticalState
        {
            get
            {
                bool result = false;
                if (this.m_Final == false && this.m_ExpectedFinalTime < DateTime.Now)
                {
                    result = true;
                }
                return result;
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
