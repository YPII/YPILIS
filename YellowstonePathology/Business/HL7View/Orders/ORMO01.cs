using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Orders
{
    public class ORMO01
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        

        public ORMO01(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public void ToXml(XElement document)
        {
			YellowstonePathology.Business.Domain.Physician casePhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo);

            MSH msh = new ARUPMSH();
            msh.ToXml(document);

            PID pid = new PID(this.m_AccessionOrder);
            pid.ToXml(document);

            PV1 pv1 = new PV1(this.m_AccessionOrder, casePhysician);
            pv1.ToXml(document);

            IN1 in1 = new IN1(this.m_AccessionOrder);
            in1.ToXml(document);

            ORC orc = new ORC(this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo, this.m_AccessionOrder, casePhysician);
            orc.ToXml(document);

			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo + ".HL7.xml";
            string mirthFileName = @"\\YPIIInterface1\ChannelData\Outgoing\Testing\" + this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo + ".HL7.xml";

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                document.Save(sw);
            }

            System.IO.File.Copy(serverFileName, mirthFileName);
        }
    }
}
