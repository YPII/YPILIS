using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICStatusMessage
    {
        private int m_ObxCount;        
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;        
        private OrderStatus m_OrderStatus;
        private YellowstonePathology.Business.ClientOrder.Model.UniversalService m_UniversalService;        
        private string m_ResultMessage;
        private string m_ResultStatus;        
        protected DateTime m_DateOfService;

        public EPICStatusMessage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, OrderStatus orderStatus, 
            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService, string resultMessage, string resultStatus, DateTime dateOfService)
		{
			this.m_ClientOrder = clientOrder;
			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.m_ClientOrder.ProviderId);
			this.m_OrderStatus = orderStatus;
            this.m_UniversalService = universalService;            
            this.m_ResultMessage = resultMessage;
            this.m_ResultStatus = resultStatus;
            this.m_DateOfService = dateOfService;          
		}
        
        public void Publish()
        {
            string fileName = System.IO.Path.Combine(@"\\YPIIInterface2\ChannelData\Outgoing\1002", this.m_ClientOrder.ClientOrderId + ".hl7.xml");
            this.PublishDocument(fileName);
        }        

        public void Publish(string basePath)
        {
            string fileName = System.IO.Path.Combine(basePath, this.m_ClientOrder.ClientOrderId + ".hl7.xml");
            this.PublishDocument(fileName);
        }

        private void PublishDocument(string fileName)
        {
            XElement document = new XElement("HL7Message");            
            this.m_ObxCount = 1;

            EPICHl7Client client = new EPICHl7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";
            if (this.m_ClientOrder.SvhMedicalRecord.StartsWith("A") == true)
            {
                locationCode = "SVHNPATH";
            }

            EPICMshView msh = new EPICMshView(client, messageType, locationCode);                
            msh.ToXml(document);

            PidView pid = new PidView(this.m_ClientOrder.SvhMedicalRecord, this.m_ClientOrder.PLastName, this.m_ClientOrder.PFirstName, this.m_ClientOrder.PBirthdate,
                this.m_ClientOrder.PSex, this.m_ClientOrder.SvhAccountNo, this.m_ClientOrder.PSSN);
            pid.ToXml(document);

            EPICStatusOrcView orc = new EPICStatusOrcView(this.m_ClientOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_OrderStatus);
            orc.ToXml(document);

            EPICStatusObrView obr = new EPICStatusObrView(this.m_ClientOrder.ExternalOrderId, string.Empty, this.m_DateOfService, null, this.m_OrderingPhysician, this.m_ResultStatus, this.m_UniversalService);
            obr.ToXml(document);

            EPICStatusObxView obx = new EPICStatusObxView(m_ObxCount, this.m_ResultStatus, this.m_ResultMessage);
            obx.ToXml(document);
            this.m_ObxCount = obx.ObxCount;                                
            
            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            //{
            //    document.Save(sw);
            //}
        }        
    }
}
