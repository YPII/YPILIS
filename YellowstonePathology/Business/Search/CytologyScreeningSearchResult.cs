using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Search
{
	public class CytologyScreeningSearchResult
	{
		string m_MasterAccessionNo;
        string m_ReportNo;
        string m_PatientName;
        string m_OrderedByName;
        string m_ScreenedByName;        
        string m_ScreeningType;
		string m_AssignedToName;
        DateTime m_AccessionTime;
        Nullable<DateTime> m_ScreeningFinalTime;
        Nullable<DateTime> m_CaseFinalTime;

        public CytologyScreeningSearchResult()
        {
            
        }

		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set { this.m_MasterAccessionNo = value; }
		}

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public string PatientName
        {
            get { return this.m_PatientName; }
            set { this.m_PatientName = value; }
        }

        public DateTime AccessionTime
        {
            get { return this.m_AccessionTime; }
            set { this.m_AccessionTime = value; }
        }

        public Nullable<DateTime> ScreeningFinalTime
        {
            get { return this.m_ScreeningFinalTime; }
            set { this.m_ScreeningFinalTime = value; }
        }        

        public Nullable<DateTime> CaseFinalTime
        {
            get { return this.m_CaseFinalTime; }
            set { this.m_CaseFinalTime = value; }
        }

        public string OrderedByName
        {
            get { return this.m_OrderedByName; }
            set { this.m_OrderedByName = value; }
        }

        public string ScreenedByName
        {
            get { return this.m_ScreenedByName; }
            set { this.m_ScreenedByName = value; }
        }

		public string AssignedToName
		{
			get { return this.m_AssignedToName; }
			set { this.m_AssignedToName = value; }
		}

        public string ScreeningType
        {
            get { return this.m_ScreeningType; }
            set { this.m_ScreeningType = value; }
        }

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_PatientName = propertyWriter.WriteString("PatientName");
			this.m_OrderedByName = propertyWriter.WriteString("OrderedByName");
			this.m_ScreenedByName = propertyWriter.WriteString("ScreenedByName");
			this.m_ScreeningType = propertyWriter.WriteString("ScreeningType");
			this.m_AssignedToName = propertyWriter.WriteString("AssignedToName");
			this.m_AccessionTime = propertyWriter.WriteDateTime("AccessionTime");
			this.m_ScreeningFinalTime = propertyWriter.WriteNullableDateTime("ScreeningFinalTime");
			this.m_CaseFinalTime = propertyWriter.WriteNullableDateTime("CaseFinalTime");
		}

        public void ToXml(XElement listElement)
        {
            string screeningFinalTime = string.Empty;
            string caseFinalTime = string.Empty;
            if (this.m_ScreeningFinalTime.HasValue) screeningFinalTime = this.m_ScreeningFinalTime.Value.ToString("MM/dd/yyy HH:mm");
            if (this.m_CaseFinalTime.HasValue) caseFinalTime = this.m_CaseFinalTime.Value.ToString("MM/dd/yyy HH:mm");

            XElement resultElement = new XElement("CytologyScreeningSearchResult",
                new XElement("MasterAccessionNo", this.MasterAccessionNo),
                new XElement("ReportNo", this.m_ReportNo),
                new XElement("PatientName", this.m_PatientName),
                new XElement("OrderedByName", this.m_OrderedByName),
                new XElement("ScreenedByName", this.m_ScreenedByName),
                new XElement("ScreeningType", this.m_ScreeningType),
                new XElement("AssignedToName", this.m_AssignedToName),
                new XElement("AccessionTime", this.m_AccessionTime.ToString("MM/dd/yyy HH:mm")),
                new XElement("ScreeningFinalTime", screeningFinalTime),
                new XElement("CaseFinalTime", caseFinalTime));

            listElement.Add(resultElement);
        }
	}
}
