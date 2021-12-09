using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
	public class PatientHistoryResult : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_PatientId;
		private string m_MasterAccessionNo;
        private string m_ReportNo;
        private int m_PanelSetId;
        private DateTime m_AccessionDate;
        private string m_PanelSetName;
        private Nullable<DateTime> m_FinalDate;
        private string m_ResultCode;

        public PatientHistoryResult()
        {
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        [PersistentPrimaryKeyProperty(false)]
        public string PatientId
		{
			get { return m_PatientId; }
			set
			{
				this.m_PatientId = value;
				NotifyPropertyChanged("PatientId");
			}
		}

        [PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return m_MasterAccessionNo; }
			set
			{
				this.m_MasterAccessionNo = value;
				NotifyPropertyChanged("MasterAccessionNo");
			}
		}


        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                this.m_ReportNo = value;
				NotifyPropertyChanged("ReportNo");
			}
        }

        [PersistentProperty()]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                this.m_PanelSetId = value;
                NotifyPropertyChanged("PanelSetId");
            }
        }

        [PersistentProperty()]
        public DateTime AccessionDate     
        {
            get { return this.m_AccessionDate; }
            set
            {
				this.m_AccessionDate = value;
				NotifyPropertyChanged("AccessionDate");
			}
        }

        [PersistentProperty()]
        public string PanelSetName
		{
			get { return this.m_PanelSetName; }
            set
            {
                this.m_PanelSetName = value;
                NotifyPropertyChanged("PanelSetName");
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
                
			set
			{
				this.m_FinalDate = value;
				NotifyPropertyChanged("FinalDate");
			}
		}

        [PersistentProperty()]
        public string ResultCode
        {
            get { return this.m_ResultCode; }
            set
            {
                this.m_ResultCode = value;
                NotifyPropertyChanged("ResultCode");
            }
        }
    }
}
