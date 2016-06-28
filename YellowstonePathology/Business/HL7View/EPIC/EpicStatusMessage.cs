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
        private XElement m_Document;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;        
        private OrderStatus m_OrderStatus;
        private YellowstonePathology.Business.ClientOrder.Model.UniversalService m_UniversalService;

        protected string m_ServerFileName;
        protected string m_InterfaceFilename;
       
        public EPICStatusMessage(string clientOrderId, OrderStatus orderStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService, object writer)
        {
            this.m_ClientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(clientOrderId, writer);            
			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.m_ClientOrder.ProviderId);
            this.m_OrderStatus = orderStatus;
            this.m_UniversalService = universalService;
            this.SetupFileNames();
        }

        public EPICStatusMessage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, OrderStatus orderStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService)
		{
			this.m_ClientOrder = clientOrder;
			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.m_ClientOrder.ProviderId);
			this.m_OrderStatus = orderStatus;
            this.m_UniversalService = universalService;
            this.SetupFileNames();
		}

        public virtual void SetupFileNames()
        {
            string newGuid = Guid.NewGuid().ToString();
            this.m_ServerFileName = @"\\YPIIInterface1\ChannelData\Outgoing\1002\In\" + newGuid + ".xml";
            this.m_InterfaceFilename = @"\\YPIIInterface1\ChannelData\Outgoing\1002\" + newGuid + ".xml";            
        }

        public YellowstonePathology.Business.Rules.MethodResult Send()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (result.Success == true)
            {
                this.m_Document = new XElement("HL7Message");
                this.m_ObxCount = 1;

                EPICHl7Client client = new EPICHl7Client();
                OruR01 messageType = new OruR01();

                string locationCode = "YPIIBILLINGS";
                if (this.m_ClientOrder.SvhMedicalRecord.StartsWith("A") == true)
                {
                    locationCode = "SVHNPATH";
                }

                EPICMshView msh = new EPICMshView(client, messageType, locationCode);                
                msh.ToXml(this.m_Document);

                PidView pid = new PidView(this.m_ClientOrder.SvhMedicalRecord, this.m_ClientOrder.PLastName, this.m_ClientOrder.PFirstName, this.m_ClientOrder.PBirthdate,
                    this.m_ClientOrder.PSex, this.m_ClientOrder.SvhAccountNo, this.m_ClientOrder.PSSN);
                pid.ToXml(this.m_Document);

                EPICStatusOrcView orc = new EPICStatusOrcView(this.m_ClientOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_OrderStatus);
                orc.ToXml(this.m_Document);

                EPICStatusObrView obr = new EPICStatusObrView(this.m_ClientOrder.ExternalOrderId, string.Empty, this.m_ClientOrder.OrderTime, null, this.m_OrderingPhysician, ResultStatusEnum.InProcess.Value, this.m_UniversalService);
                obr.ToXml(this.m_Document);

                EPICStatusObxView obx = new EPICStatusObxView(m_ObxCount);
                obx.ToXml(this.m_Document);
                this.m_ObxCount = obx.ObxCount;                

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(this.m_ServerFileName))
                {
                    this.m_Document.Save(sw);
                }

                System.IO.File.Copy(this.m_ServerFileName, this.m_InterfaceFilename);
            }
            return result;
        }

        private YellowstonePathology.Business.Rules.MethodResult OkToSend()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();

            if (string.IsNullOrEmpty(this.m_ClientOrder.UniversalServiceId) == true)
            {
                result.Success = false;
                result.Message = "The universal service id is null";
            }

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceNone universalServiceNone = new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceNone();
            if (this.m_UniversalService.ApplicationName == universalServiceNone.ApplicationName)
            {
                result.Success = false;
                result.Message = "The universal service is NONE.";
            }

            return result;
        }
    }
}
