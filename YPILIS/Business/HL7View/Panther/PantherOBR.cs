using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Panther
{
	public class PantherOBR
	{                        
        private string m_ReportNo;                
        private PantherAssay m_Assay;
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

        public PantherOBR(string reportNo, PantherAssay assay, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {                                 
            this.m_ReportNo = reportNo;            
            this.m_Assay = assay;
            this.m_SpecimenOrder = specimenOrder;
        }               

        public void ToXml(XElement document)
        {            
            XElement obrElement = new XElement("OBR");
            document.Add(obrElement);

            XElement obr01Element = new XElement("OBR.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.1.1", "1", obr01Element);            
            obrElement.Add(obr01Element);            

            XElement obr03Element = new XElement("OBR.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.3.1", this.m_ReportNo, obr03Element);            
            obrElement.Add(obr03Element);            

            XElement obr04Element = new XElement("OBR.4");                        
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.4.1", this.m_Assay.ShortName, obr04Element);            
            obrElement.Add(obr04Element);

            XElement obr15Element = new XElement("OBR.15");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.15.1", this.m_SpecimenOrder.Description, obr15Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.15.2", this.m_SpecimenOrder.SpecimenSource, obr15Element);
            obrElement.Add(obr15Element);            

            XElement obr27Element = new XElement("OBR.27");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBR.27.1", "^^^^^" + PantherOrderPriority.Routine, obr27Element);            
            obrElement.Add(obr27Element);			            
        }        
	}
}
