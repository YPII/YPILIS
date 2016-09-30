using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

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

		public string PatientId
		{
			get { return m_PatientId; }
			set
			{
				this.m_PatientId = value;
				NotifyPropertyChanged("PatientId");
			}
		}

		public string MasterAccessionNo
		{
			get { return m_MasterAccessionNo; }
			set
			{
				this.m_MasterAccessionNo = value;
				NotifyPropertyChanged("MasterAccessionNo");
			}
		}


        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                this.m_ReportNo = value;
				NotifyPropertyChanged("ReportNo");
			}
        }

        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                this.m_PanelSetId = value;
                NotifyPropertyChanged("PanelSetId");
            }
        }

        public DateTime AccessionDate     
        {
            get { return this.m_AccessionDate; }
            set
            {
				this.m_AccessionDate = value;
				NotifyPropertyChanged("AccessionDate");
			}
        }

		public string PanelSetName
		{
			get { return this.m_PanelSetName; }
		}

		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
                
			set
			{
				this.m_FinalDate = value;
				NotifyPropertyChanged("FinalDate");
			}
		}

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_PatientId = propertyWriter.WriteString("PatientId");
			this.m_AccessionDate = propertyWriter.WriteDateTime("AccessionDate");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
            this.m_PanelSetId = propertyWriter.WriteInt("PanelSetId");
			this.m_PanelSetName = propertyWriter.WriteString("PanelSetName");
			this.m_FinalDate = propertyWriter.WriteNullableDateTime("FinalDate");
		}
		#endregion
    }
}
