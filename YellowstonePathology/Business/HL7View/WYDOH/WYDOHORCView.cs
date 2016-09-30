using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WYDOH
{
	public class WYDOHORCView
	{        		
		private string m_DateFormat = "yyyyMMddHHmm";
		private string m_ReportNo;        
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private OrderStatus m_OrderStatus;
        private string m_ExternalOrderId;
        private string m_SystemInitiatingOrder;

		public WYDOHORCView(string externalOrderId, YellowstonePathology.Business.Domain.Physician orderingPhysician, string reportNo, OrderStatus orderStatus, string systemInitiatingOrder)
        {
            this.m_ExternalOrderId = externalOrderId;
            this.m_OrderingPhysician = orderingPhysician;
            this.m_ReportNo = reportNo;
            this.m_OrderStatus = orderStatus;
            this.m_SystemInitiatingOrder = systemInitiatingOrder;
        }       

        public void ToXml(XElement document)
        {
            XElement orcElement = new XElement("ORC");
            document.Add(orcElement);

            XElement orc01Element = new XElement("ORC.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.1.1", "RE", orc01Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc01Element);

            if (string.IsNullOrEmpty(this.m_ExternalOrderId) == false)
            {
                XElement orc02Element = new XElement("ORC.2");
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.2.1", this.m_ExternalOrderId, orc02Element);                
                YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc02Element);
            }

            XElement orc03Element = new XElement("ORC.3");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.3.1", this.m_ReportNo, orc03Element);
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
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.4",this.m_OrderingPhysician.GetNormalizedMiddleInitial() , orc12Element);            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.9","NPI" , orc12Element);            
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc12Element);

			XElement orc21Element = new XElement("ORC.21");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.21.1", "2900 12th Avenue North, Suite 295W", orc21Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.21.3", "Billings", orc21Element);
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElementIfNotEmpty(orcElement, orc21Element);
		}
	}
}
