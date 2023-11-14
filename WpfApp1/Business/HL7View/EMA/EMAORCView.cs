using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EMA
{
	public class EMAORCView
	{        		
		private string m_DateFormat = "yyyyMMddHHmm";
		private string m_MasterAccessionNo;                        
        private string m_EmaSpecimenOrderId;
        private string m_EmaOrderId;        

        public EMAORCView(string emaSpecimenOrderId, string emaOrderId, string masterAccessionNo)
        {
            this.m_EmaSpecimenOrderId = emaSpecimenOrderId;
            this.m_EmaOrderId = emaOrderId;
            this.m_MasterAccessionNo = masterAccessionNo;            
        }       

        public void ToXml(XElement document)
        {
            XElement orcElement = new XElement("ORC");
            document.Add(orcElement);

            XElement orc01Element = new XElement("ORC.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.1.1", "RE", orc01Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc01Element);
            
            XElement orc02Element = new XElement("ORC.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.2.1", this.m_EmaSpecimenOrderId, orc02Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc02Element);
            

            XElement orc03Element = new XElement("ORC.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.3.1", this.m_MasterAccessionNo, orc03Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.3.2", "YPILIS", orc03Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc03Element);                        
            
            XElement orc04Element = new XElement("ORC.4");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.4.1", this.m_EmaOrderId, orc04Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc04Element);                        
        }
	}
}
