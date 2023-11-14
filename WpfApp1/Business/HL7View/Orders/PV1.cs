using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Orders
{
    public class PV1
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Domain.Physician m_AttendingPhysician;

        public PV1(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Domain.Physician attendingPhysician)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_AttendingPhysician = attendingPhysician;
        }

        public void ToXml(XElement document)
        {
            XElement pv1Element = new XElement("PV1");

            XElement pv101Element = new XElement("PV1.1");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.1.1", "1", pv101Element);
            pv1Element.Add(pv101Element);

            string patientType = "OP";
            if (this.m_AccessionOrder.PatientType == "IP") patientType = "IP";

            XElement pv102Element = new XElement("PV1.2");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.2.1", patientType, pv102Element);
            pv1Element.Add(pv102Element);

            XElement pv107Element = new XElement("PV1.7");
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.1", this.m_AttendingPhysician.Npi.ToString(), pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.2", this.m_AttendingPhysician.LastName, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.3", this.m_AttendingPhysician.FirstName, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.4", this.m_AttendingPhysician.MiddleInitial, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.5", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.6", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.7", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.8", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.9", "NPI", pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.10", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.11", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.12", string.Empty, pv107Element);
            YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("PV1.7.13", "NPI", pv107Element);
            pv1Element.Add(pv107Element);         

            document.Add(pv1Element);
        }
    }
}
