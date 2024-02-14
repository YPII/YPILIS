using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Orders
{
    public class ORC
    {
        string m_ReportNo;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public ORC(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Domain.Physician orderingPhysician)
        {
            this.m_ReportNo = reportNo;
            this.m_AccessionOrder = accessionOrder;
            this.m_OrderingPhysician = orderingPhysician;
        }

        public void ToXml(XElement document)
        {
            XElement orcElement = new XElement("ORC");
            document.Add(orcElement);

            XElement orc01Element = new XElement("ORC.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.1.1", "NW", orc01Element);

            XElement orc02Element = new XElement("ORC.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.2.1", this.m_ReportNo, orc01Element);

            XElement pv107Element = new XElement("PV1.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.1", this.m_OrderingPhysician.Npi.ToString(), pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.2", this.m_OrderingPhysician.LastName, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.3", this.m_OrderingPhysician.FirstName, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.4", this.m_OrderingPhysician.MiddleInitial, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.5", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.6", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.7", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.8", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.9", "NPI", pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.10", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.11", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.12", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("ORC.12.13", "NPI", pv107Element);
            //pv1Element.Add(pv107Element);         
        }
    }
}
