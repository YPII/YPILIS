using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI
{
	public class RetrospectiveReviewListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_MasterAccessionNo;
		private string m_ReportNo;		
		private int m_PanelSetId;
        private DateTime m_OrderDate;
		private Nullable<DateTime> m_FinalDate;
		private string m_PanelSetName;
        private string m_SurgicalFinaledBy;
        private DateTime m_SurgicalFinalDate;
        private string m_SurgicalReportNo;

        public RetrospectiveReviewListItem()
		{
		}		

        [PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

        [PersistentProperty()]
        public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}       

        [PersistentProperty()]
        public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (value != this.m_PanelSetId)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}

        [PersistentProperty()]
        public DateTime OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                if (value != this.m_OrderDate)
                {
                    this.m_OrderDate = value;
                    this.NotifyPropertyChanged("OrderDate");
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
        public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (value != this.m_PanelSetName)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}

        [PersistentProperty()]
        public string SurgicalFinaledBy
        {
            get { return this.m_SurgicalFinaledBy; }
            set
            {
                if (value != this.m_SurgicalFinaledBy)
                {
                    this.m_SurgicalFinaledBy = value;
                    this.NotifyPropertyChanged("SurgicalFinaledBy");
                }
            }
        }

        [PersistentProperty()]
        public DateTime SurgicalFinalDate
        {
            get { return this.m_SurgicalFinalDate; }
            set
            {
                if (value != this.m_SurgicalFinalDate)
                {
                    this.m_SurgicalFinalDate = value;
                    this.NotifyPropertyChanged("SurgicalFinalDate");
                }
            }
        }

        [PersistentProperty()]
        public string SurgicalReportNo
        {
            get { return this.m_SurgicalReportNo; }
            set
            {
                if (value != this.m_SurgicalReportNo)
                {
                    this.m_SurgicalReportNo = value;
                    this.NotifyPropertyChanged("SurgicalReportNo");
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
