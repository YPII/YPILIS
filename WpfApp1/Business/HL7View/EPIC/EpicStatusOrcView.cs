using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
	public class EPICStatusOrcView
	{        		
		private string m_DateFormat = "yyyyMMddHHmm";		
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private OrderStatus m_OrderStatus;
        private string m_ExternalOrderId;

        public EPICStatusOrcView(string externalOrderId, YellowstonePathology.Business.Domain.Physician orderingPhysician, OrderStatus orderStatus)
        {
            this.m_ExternalOrderId = externalOrderId;            
            this.m_OrderingPhysician = orderingPhysician;            
            this.m_OrderStatus = orderStatus;
        }		

        public void ToXml(XElement document)
        {
            XElement orcElement = new XElement("ORC");
            XElement orc01Element = new XElement("ORC.1");
            XElement orc0101Element = new XElement("ORC.1.1", "RE");
            orc01Element.Add(orc0101Element);
            orcElement.Add(orc01Element);            

            XElement orc02Element = new XElement("ORC.2");
            XElement orc0201Element = new XElement("ORC.2.1", this.m_ExternalOrderId);
            XElement orc0202Element = new XElement("ORC.2.2", "EPC");
            orc02Element.Add(orc0201Element);
            orc02Element.Add(orc0202Element);
            orcElement.Add(orc02Element);                        

            XElement orc05Element = new XElement("ORC.5");
            XElement orc0501Element = new XElement("ORC.5.1", this.m_OrderStatus.Value);
            orc05Element.Add(orc0501Element);
            orcElement.Add(orc05Element);

            XElement orc09Element = new XElement("ORC.9");
            XElement orc0901Element = new XElement("ORC.9.1", DateTime.Now.ToString(m_DateFormat));
            orc09Element.Add(orc0901Element);
            orcElement.Add(orc09Element);            

            XElement orc12Element = new XElement("ORC.12");
            XElement orc1201Element = new XElement("ORC.12.1", this.m_OrderingPhysician.Npi.ToString());
            XElement orc1202Element = new XElement("ORC.12.2", this.m_OrderingPhysician.LastName);
            XElement orc1203Element = new XElement("ORC.12.3", this.m_OrderingPhysician.FirstName);
            XElement orc1204Element = new XElement("ORC.12.4", this.m_OrderingPhysician.MiddleInitial);
            XElement orc1209Element = new XElement("ORC.12.9", "NPI");
            XElement orc1213Element = new XElement("ORC.12.13", "NPI");
            orc12Element.Add(orc1201Element);
            orc12Element.Add(orc1202Element);
            orc12Element.Add(orc1203Element);
            orc12Element.Add(orc1204Element);
            orc12Element.Add(orc1209Element);
            orc12Element.Add(orc1213Element);
            orcElement.Add(orc12Element);

            document.Add(orcElement);
        }
	}
}
