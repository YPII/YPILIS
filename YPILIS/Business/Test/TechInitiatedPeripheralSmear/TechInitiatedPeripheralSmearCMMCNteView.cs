using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
    public class TechInitiatedPeripheralSmearCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public TechInitiatedPeripheralSmearCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            TechInitiatedPeripheralSmearTestOrder testOrder = (TechInitiatedPeripheralSmearTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Tech Initiated Peripheral Smear", document);
            this.AddNextNteElement("Master Accession #: " + testOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + testOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.HandleLongString("Technologists Question: " + testOrder.TechnologistsQuestion, document);
            this.HandleLongString("Pathologist Feedback: " + testOrder.PathologistFeedback, document);
            this.HandleLongString("CBC Comment: " + testOrder.CBCComment, document);
            this.AddBlankNteElement(document);
        }
    }
}
