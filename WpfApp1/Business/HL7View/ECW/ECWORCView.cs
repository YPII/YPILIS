using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View
{
	public class ECWORCView
	{        		
		private string m_DateFormat = "yyyyMMddHHmm";
		private string m_MasterAccessionNo;        
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private OrderStatus m_OrderStatus;
        private string m_ExternalOrderId;
        private string m_SystemInitiatingOrder;
        private bool m_SendUnsolicited;

        public ECWORCView(string externalOrderId, YellowstonePathology.Business.Domain.Physician orderingPhysician, string masterAccessionNo, OrderStatus orderStatus, string systemInitiatingOrder, bool sendUnsolicited)
        {
            this.m_ExternalOrderId = externalOrderId;
            this.m_OrderingPhysician = orderingPhysician;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_OrderStatus = orderStatus;
            this.m_SystemInitiatingOrder = systemInitiatingOrder;
            this.m_SendUnsolicited = sendUnsolicited;
        }       

        public void ToXml(XElement document)
        {
            XElement orcElement = new XElement("ORC");
            document.Add(orcElement);

            XElement orc01Element = new XElement("ORC.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.1.1", "NW", orc01Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc01Element);
            
            XElement orc02Element = new XElement("ORC.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.2.1", this.m_ExternalOrderId, orc02Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc02Element);
            

            XElement orc03Element = new XElement("ORC.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.3.1", this.m_MasterAccessionNo, orc03Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.3.2", "YPILIS", orc03Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc03Element);                        
            
            XElement orc05Element = new XElement("ORC.5");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.5.1", this.m_OrderStatus.Value, orc05Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc05Element);            

            XElement orc09Element = new XElement("ORC.9");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.9.1", DateTime.Now.ToString(m_DateFormat), orc09Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc09Element);            

            XElement orc12Element = new XElement("ORC.12");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.1",this.m_OrderingPhysician.Npi.ToString() , orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.2",this.m_OrderingPhysician.LastName , orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.3",this.m_OrderingPhysician.FirstName , orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.4",this.m_OrderingPhysician.MiddleInitial , orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.5", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.6", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.7", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.8", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.9","NPI" , orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.10", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.11", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.12", string.Empty, orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.13","NPI" , orc12Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc12Element);           
        }
	}
}
