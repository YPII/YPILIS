using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View
{
    public class BigSkyDermatologyResultView
    {
        XElement m_Document;
        int m_ObxCount;
        string m_ReportNo;
        OrderStatus m_OrderStatus;

        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public BigSkyDermatologyResultView(string reportNo, OrderStatus orderStatus, object writer)
        {
            this.m_ReportNo = reportNo;
            this.m_OrderStatus = orderStatus;

            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(this.m_ReportNo);
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, writer);

			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);           
		}        

        public bool Send(string fileName)
        {            
            bool result = true;
            if (this.OkToSend() == true)
            {
                this.m_Document = new XElement("HL7Message");
                this.m_ObxCount = 1;

                BigSkyDermatology client = new BigSkyDermatology();
                OruR01 messageType = new OruR01();

                MshView msh = new MshView(client, messageType);
                msh.ToXml(this.m_Document);

                PidView pid = new PidView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                    this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN);
                pid.ToXml(this.m_Document);
                
                //OrcView orc = new OrcView(this.m_AccessionOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_ReportNo, this.m_OrderStatus);
                //orc.ToXml(this.m_Document);

                //ObrView obr = new ObrView(this.m_AccessionOrder, this.m_ReportNo);
                //obr.ToXml(this.m_Document);

                SurgicalObxView obx = new SurgicalObxView(this.m_AccessionOrder, this.m_ReportNo, this.m_ObxCount);
                obx.ToXml(this.m_Document);
                this.m_ObxCount = obx.ObxCount;

                System.IO.StreamWriter sw = System.IO.File.CreateText(fileName);
                this.m_Document.Save(sw);
            }
            return result;
        }

        public bool OkToSend()
        {
            bool result = true;
            return result;
        }
    }
}
