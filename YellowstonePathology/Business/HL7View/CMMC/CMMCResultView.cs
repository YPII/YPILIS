using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CMMC
{
	public class CMMCResultView : IResultView
	{
        private XElement m_Document;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;        

		public CMMCResultView(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);            

            this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);
		}

        public void Send(YellowstonePathology.Business.Rules.MethodResult result)
        {
            this.CreateDocument();
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + ".HL7.xml";
            string mirthFileName = @"\\YPIIInterface1\ChannelData\Outgoing\1003\" + this.m_PanelSetOrder.ReportNo + ".HL7.xml";
            //string mirthFileName = @"\\YPIIInterface1\ChannelData\Outgoing\Testing\" + this.m_ReportNo + ".HL7.xml";

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                this.m_Document.Save(sw);
            }

            System.IO.File.Copy(serverFileName, mirthFileName);

            result.Success = true;
            result.Message = "An HL7 message was created and sent to the interface.";
        }

        private void CreateDocument()
        {
            this.m_Document = new XElement("HL7Message");

            CMMCHl7Client client = new CMMCHl7Client();
            OruR01 messageType = new OruR01();

            CMMCMshView msh = new CMMCMshView(client, messageType, "YPII");
            msh.ToXml(this.m_Document);

            CMMCPidView pid = new CMMCPidView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN);
            pid.ToXml(this.m_Document);

            CMMCOrcView orc = new CMMCOrcView(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            orc.ToXml(this.m_Document);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection[0];
            CMMCObrView obr = new CMMCObrView(this.m_AccessionOrder.ExternalOrderId, this.m_PanelSetOrder.ReportNo, specimenOrder.CollectionTime, this.m_AccessionOrder.AccessionDateTime, this.m_AccessionOrder.AccessionDate, this.m_OrderingPhysician, ResultStatusEnum.Final.Value);
            obr.ToXml(this.m_Document);

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);            
            CMMCNteView nteView = CMMCNteViewFactory.GetNteView(panelSetOrder.PanelSetId, this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
            nteView.ToXml(this.m_Document);            
        }

        public void CanSend(YellowstonePathology.Business.Rules.MethodResult result)
        {            
            string message = "Unable to send the message due to the following: ";
            if (string.IsNullOrEmpty(this.m_OrderingPhysician.Npi) == true)
            {
                message += "The provider NPI is not set.";
                result.Success = false;                
            }
            if (result.Success == false) result.Message = message;
        }        

        public XElement GetDocument()
        {
            this.CreateDocument();
            return this.m_Document;
        }
	}
}
