using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Flow
{
	public class FlowLogListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_ReportNo;
		private string m_PLastName;
		private string m_PFirstName;
		private Nullable<DateTime> m_AccessionDate;
		private Nullable<DateTime> m_FinalDate;
		private string m_TestName;
		private string m_MasterAccessionNo;
        private bool m_IsLockAquiredByMe;
        private bool m_LockAquired;

        public FlowLogListItem()
        {
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set { this.m_ObjectId = value; }
		}

		[PersistentProperty()]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set { this.m_ReportNo = value; }
		}

		[PersistentProperty()]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if (value != this.m_PLastName)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
				}
			}
		}

		[PersistentProperty()]
		public string PFirstName
		{
			get { return this.m_PFirstName; }
			set
			{
				if (value != this.m_PFirstName)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if (value != this.m_AccessionDate)
				{
					this.m_AccessionDate = value;
					this.NotifyPropertyChanged("AccessionDate");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if (value != this.m_FinalDate)
				{
					this.m_FinalDate = value;
					this.NotifyPropertyChanged("FinalDate");
				}
			}
		}

		[PersistentProperty()]
		public string TestName
		{
			get { return this.m_TestName; }
			set
			{
				if (value != this.m_TestName)
				{
					this.m_TestName = value;
					this.NotifyPropertyChanged("TestName");
				}
			}
		}

		[PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set { this.m_MasterAccessionNo = value; }
		}

        public bool IsLockAquiredByMe
        {
            get { return this.m_IsLockAquiredByMe; }
            set
            {
                if (value != this.m_IsLockAquiredByMe)
                {
                    this.m_IsLockAquiredByMe = value;
                    this.NotifyPropertyChanged("IsLockAquiredByMe");
                }
            }
        }

        public bool LockAquired
        {
            get { return this.m_LockAquired; }
            set
            {
                if (value != this.m_LockAquired)
                {
                    this.m_LockAquired = value;
                    this.NotifyPropertyChanged("LockAquired");
                }
            }
        }
    }
}
