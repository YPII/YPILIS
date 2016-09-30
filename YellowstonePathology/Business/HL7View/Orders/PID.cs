using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Orders
{
	public class PID
	{
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public PID(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;    
        }        

        public void ToXml(XElement document)
        {                       
            XElement pidElement = new XElement("PID");
            document.Add(pidElement);

            XElement pid01Element = new XElement("PID.1");
            XElement pid0101Element = new XElement("PID.1.1", "1");
            pid01Element.Add(pid0101Element);            
            pidElement.Add(pid01Element);            

            XElement pid03Element = new XElement("PID.3");
            XElement pid0301Element = new XElement("PID.3.1", this.m_AccessionOrder.PatientId);            
            XElement pid0305Element = new XElement("PID.3.5", "PI");
            XElement pid0306Element = new XElement("PID.3.6", "Yellowstone Pathology&27D0946844&CLIA");                        

            pid03Element.Add(pid0301Element);            
            pid03Element.Add(pid0305Element);
            pid03Element.Add(pid0306Element);

            pidElement.Add(pid03Element);                                   

            XElement pid05Element = new XElement("PID.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.5.1", this.m_AccessionOrder.PLastName, pid05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.5.2", this.m_AccessionOrder.PFirstName, pid05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid05Element);

            XElement pid07Element = new XElement("PID.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.7.1", this.m_AccessionOrder.PBirthdate.Value.ToString("yyyyMMdd"), pid07Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid07Element);

            XElement pid08Element = new XElement("PID.8");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.8.1", this.m_AccessionOrder.PSex, pid08Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid08Element);

            XElement pid11Element = new XElement("PID.11");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.1", this.m_AccessionOrder.PAddress1, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.2", this.m_AccessionOrder.PAddress2, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.3", this.m_AccessionOrder.PCity, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.4", this.m_AccessionOrder.PState, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.11.5", this.m_AccessionOrder.PZipCode, pid11Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid11Element);                        

            XElement pid19Element = new XElement("PID.19");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PID.19.1", YellowstonePathology.Business.Helper.SsnHelper.FormatWithoutDashes(this.m_AccessionOrder.PSSN), pid19Element);                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(pidElement, pid19Element);
        }        
	}
}
