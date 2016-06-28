using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WYDOH
{
	public class WYDOHPIDView
	{
        string m_DateFormat = "yyyyMMdd";

        string m_PatientId;
        string m_LastName;
        string m_FirstName;
        Nullable<DateTime> m_Birthdate;
        string m_Sex;
        string m_PatientAccountNumber;
        string m_Ssn;
        string m_Address1;
        string m_Address2;
        string m_City;
        string m_State;
        string m_Zip;

        public WYDOHPIDView(string patientId, string lastName, string firstName, Nullable<DateTime> birthdate,
            string sex, string patientAccountNumber, string ssn, string address1, string address2, string city, string state, string zip)
        {
            this.m_PatientId = patientId;
            this.m_LastName = lastName;
            this.m_FirstName = firstName;
            this.m_Birthdate = birthdate;
            this.m_Sex = sex;
            this.m_PatientAccountNumber = patientAccountNumber;
            this.m_Ssn = ssn;
            this.m_Address1 = address1;
            this.m_Address2 = address2;
            this.m_City = city;
            this.m_State = state;
            this.m_Zip = zip;
        }

        public void ToXml(XElement document)
        {
            XElement pidElement = new XElement("PID");
            document.Add(pidElement);

            XElement pid03Element = new XElement("PID.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.3.1", this.m_PatientId, pid03Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid03Element);

            XElement pid05Element = new XElement("PID.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.5.1", this.m_LastName, pid05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.5.2", this.m_FirstName, pid05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid05Element);

            XElement pid07Element = new XElement("PID.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.7.1", this.m_Birthdate.Value.ToString(this.m_DateFormat), pid07Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid07Element);

            XElement pid08Element = new XElement("PID.8");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.8.1", this.m_Sex, pid08Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid08Element);

            XElement pid11Element = new XElement("PID.11");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.1", this.m_Address1, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.2", this.m_Address2, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.3", this.m_City, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.4", this.m_State, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.5", this.m_Zip, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid11Element);

            XElement pid18Element = new XElement("PID.18");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.18.1", this.m_PatientAccountNumber, pid18Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid18Element);

            XElement pid19Element = new XElement("PID.19");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.19.1", YellowstonePathology.Business.Helper.SsnHelper.FormatWithoutDashes(this.m_Ssn), pid19Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid19Element);
        }        
	}
}
