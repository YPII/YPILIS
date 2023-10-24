using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EMA
{
	public class EMAOBRView
	{        
        private string m_ExternalOrderId;
        private string m_DateFormat = "yyyyMMddHHmm";

        private string m_YpiMasterAccessionNo;
        private string m_EmaSpecimenOrderId;
        YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public EMAOBRView(string emaSpecimenOrderId, string ypiMasterAccessionNo, YellowstonePathology.Business.Domain.Physician orderingPhysician)
        {
            this.m_EmaSpecimenOrderId = emaSpecimenOrderId;
            this.m_YpiMasterAccessionNo = ypiMasterAccessionNo;
            this.m_OrderingPhysician = orderingPhysician;
        }               

        public void ToXml(XElement document)
        {            
            XElement obrElement = new XElement("OBR");
            document.Add(obrElement);

            XElement obr01Element = new XElement("OBR.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.1.1", "1", obr01Element);            
            obrElement.Add(obr01Element);
            
            XElement obr02Element = new XElement("OBR.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.2.1", this.m_EmaSpecimenOrderId, obr02Element);
            obrElement.Add(obr02Element);

            XElement obr03Element = new XElement("OBR.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.3.1", this.m_YpiMasterAccessionNo, obr03Element);
            obrElement.Add(obr03Element);

            XElement obr04Element = new XElement("OBR.4");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.1", "D48.5^Biopsy by Shave Method H and E", obr04Element);            
            obrElement.Add(obr04Element);

            XElement obr16Element = new XElement("OBR.16");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.1", this.m_OrderingPhysician.Npi.ToString(), obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.2", this.m_OrderingPhysician.LastName, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.3", this.m_OrderingPhysician.FirstName, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.4", this.m_OrderingPhysician.MiddleInitial, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.5", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.6", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.7", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.8", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.9", "NPI", obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.10", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.11", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.12", string.Empty, obr16Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.16.13", "NPI", obr16Element);
            obrElement.Add(obr16Element);

            XElement obr25Element = new XElement("OBR.25");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.25.1", "F", obr25Element);
            obrElement.Add(obr25Element);
        }        
	}
}
