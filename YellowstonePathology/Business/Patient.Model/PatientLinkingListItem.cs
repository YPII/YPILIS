using System;
using System.Xml.Linq;
using System.ComponentModel;

namespace YellowstonePathology.Business.Patient.Model
{
    public class PatientLinkingListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private bool m_IsSelected;
		private string m_MasterAccessionNo;
        private string m_ReportNo;
        private string m_PatientId;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_PMiddleInitial;
        private string m_PSSN;
        private Nullable<DateTime> m_PBirthdate;
        private DateTime m_AccessionDate;
        private string m_MatchStatus;

        public PatientLinkingListItem()
        {

        }

		public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
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
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
			{
				m_IsSelected = value;
				NotifyPropertyChanged("IsSelected");
			}
        }                
       
        public string PatientId
        {
            get { return m_PatientId; }
            set
			{
				m_PatientId = value;
				NotifyPropertyChanged("PatientId");
			}
        }

        public string PLastName
        {
			get { return m_PLastName; }
            set
			{
				m_PLastName = value;
				NotifyPropertyChanged("PLastName");
			}
        }

        public string PFirstName
        {
			get { return m_PFirstName; }
            set
			{
				m_PFirstName = value;
				NotifyPropertyChanged("PFirstName");
			}
        }        

        public string PMiddleInitial
        {
			get { return m_PMiddleInitial; }
            set
			{
				m_PMiddleInitial = value;
				NotifyPropertyChanged("PMiddleInitial");
			}
        }

        public string PSSN
        {
			get { return m_PSSN; }
            set
			{
				m_PSSN = value;
				NotifyPropertyChanged("PSSN");
			}
        }

        public Nullable<DateTime> PBirthdate
        {
            get { return m_PBirthdate; }
            set
			{
				m_PBirthdate = value;
				NotifyPropertyChanged("PBirthdate");
			}
        }
       
        public Nullable<DateTime> AccessionDate
        {
            get
            {
                Nullable<DateTime> dt = m_AccessionDate;
                return dt;
            }
            set
            {
                m_AccessionDate = value.Value;
				NotifyPropertyChanged("AccessionDate");
			}
        }

        public string MatchStatus
        {
            get { return this.m_MatchStatus; }
            set
            {
                this.m_MatchStatus = value;
                NotifyPropertyChanged("MatchStatus");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public YellowstonePathology.Business.Validation.ValidationResult IsOkToLink()
		{
			YellowstonePathology.Business.Validation.ValidationResult result = new Business.Validation.ValidationResult();
			result.IsValid = true;
			result.Message = string.Empty;
			if (string.IsNullOrEmpty(this.m_PLastName) == true)
			{
				result.IsValid = false;
				result.Message += "The patient last name is required.\r\n";
			}
			if (string.IsNullOrEmpty(this.m_PFirstName) == true)
			{
				result.IsValid = false;
				result.Message += "The patient first name is required.\r\n";
			}
			if (this.m_PBirthdate.HasValue == false)
			{
				result.IsValid = false;
				result.Message += "The patient birthdate is required.\r\n";
			}
			return result;
		}

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_PatientId = propertyWriter.WriteString("PatientId");
			this.m_PLastName = propertyWriter.WriteString("PLastName");
			this.m_PFirstName = propertyWriter.WriteString("PFirstName");
			this.m_PMiddleInitial = propertyWriter.WriteString("PMiddleInitial");
			this.m_PSSN = propertyWriter.WriteString("PSSN");
			this.m_PBirthdate = propertyWriter.WriteNullableDateTime("PBirthdate");
			this.m_AccessionDate = propertyWriter.WriteDateTime("AccessionDate");
		}

		public XElement ToXml()
		{
			Nullable<DateTime> nullDate = null;
			XElement finalDateElement = new XElement("FinalDate");
			YellowstonePathology.Business.Helper.DateTimeExtensions.ToXmlString(finalDateElement, nullDate);

			XElement patientHistoryResultElement = new XElement("PatientHistoryResult",
				new XElement("PatientId", this.m_PatientId),
				new XElement("MasterAccessionNo", this.m_MasterAccessionNo),
				new XElement("ReportNo", this.m_ReportNo),
				new XElement("AccessionDate", YellowstonePathology.Business.Helper.DateTimeExtensions.ToXmlString(this.m_AccessionDate)),
				finalDateElement,
				new XElement("Results",
					new XElement("Result",
					new XElement("TestName"),
					new XElement("Text"))));

			return patientHistoryResultElement;
		}
	}
}
