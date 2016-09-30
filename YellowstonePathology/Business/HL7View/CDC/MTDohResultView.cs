using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CDC
{
    public class MTDohResultView : IResultView
    {
        public static string CLIANUMBER = "27D0946844";

        private XElement m_Document;
        private int m_ObxCount;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;        

        public MTDohResultView(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;            
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);           
		}

        public void Send(YellowstonePathology.Business.Rules.MethodResult result)
        {                        
            this.m_Document = new XElement("HL7Message");
            this.m_ObxCount = 1;

            MTDoh client = new MTDoh();
            OruR01 messageType = new OruR01();

            MTDohMshView msh = new MTDohMshView(client, messageType);
            msh.ToXml(this.m_Document);

            MTDohPidView pid = new MTDohPidView(this.m_AccessionOrder.PatientId, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN, this.m_AccessionOrder.PAddress1, this.m_AccessionOrder.PAddress2,
                this.m_AccessionOrder.PCity, this.m_AccessionOrder.PState, this.m_AccessionOrder.PZipCode);
            pid.ToXml(this.m_Document);
            
            MTDohOrcView orc = new MTDohOrcView(this.m_AccessionOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_PanelSetOrder.ReportNo, OrderStatusEnum.Complete, this.m_AccessionOrder.SystemInitiatingOrder);
            orc.ToXml(this.m_Document);

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
            ResultStatus resultStatus = ResultStatusEnum.Final;
            if (panelSetOrder.AmendmentCollection.Count != 0) resultStatus = ResultStatusEnum.Correction;
            MTDohObrView obr = new MTDohObrView(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_OrderingPhysician);
            obr.ToXml(this.m_Document);

            MTDohObxView obx = new MTDohObxView(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_ObxCount);
            obx.ToXml(this.m_Document);
            this.m_ObxCount = obx.ObxCount;

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + ".Mirth.xml";

            string mirthFileName = mirthFileName = @"\\YPIIInterface1\ChannelData\Outgoing\1004\" + this.m_PanelSetOrder.ReportNo + ".Mirth.xml";

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(serverFileName, false, new ASCIIEncoding());
            this.m_Document.Save(streamWriter);
            streamWriter.Close();
            System.IO.File.Copy(serverFileName, mirthFileName, true);

            result.Success = true;
            result.Message = "An HL7 message was created and sent to the interface.";
        }

        public void CanSend(YellowstonePathology.Business.Rules.MethodResult result)
        {        
            string message = "Unable to send the message due to the following: ";
            if (string.IsNullOrEmpty(this.m_OrderingPhysician.Npi) == true)
            {
                message += "The provider NPI is null or 0.";
                result.Success = false;
            }
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PAddress1) == true)
            {
                message += "The address is empty.";
                result.Success = false;
            }
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PCity) == true)
            {
                message += "The city is empty.";
                result.Success = false;
            }
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PState) == true)
            {
                message += "The state is empty.";
                result.Success = false;
            }
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PZipCode) == true)
            {
                message += "The zipcode is empty.";
                result.Success = false;
            }

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
            if (panelSetOrder.PanelSetId != 13)
            {
                message += "This is not a surgical case.";
                result.Success = false;
            }
            if (result.Success == false)
            {
                result.Message = message;
            }         
        }

        public XElement GetDocument()
        {
            return null;
        }        
	}
}
