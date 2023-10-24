using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICBeakerObxViewPDF
    {        
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        protected string m_ReportNo;    

        public EPICBeakerObxViewPDF(Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;			
        }                        
               
        public void AddPDFSegments(string fileName, XElement document)
        {            
            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            string base64 = Convert.ToBase64String(bytes);                                  

            XElement obxElement = new XElement("OBX");
            document.Add(obxElement);            

            XElement obx05Element = new XElement("OBX.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.2", "PDF", obx05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.4", "Base64", obx05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.5", base64, obx05Element);
            obxElement.Add(obx05Element);
        }                
    }
}
