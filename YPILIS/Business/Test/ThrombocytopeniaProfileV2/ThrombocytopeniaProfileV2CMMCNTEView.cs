using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    public class ThrombocytopeniaProfileV2CMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public ThrombocytopeniaProfileV2CMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            ThrombocytopeniaProfileV2.ThrombocytopeniaProfileV2TestOrder panelSetOrder = (ThrombocytopeniaProfileV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Test: Anti-Platelet Antibody - IgG", document);
            this.AddNextNteElement("Result: " + panelSetOrder.AntiPlateletAntibodyIgG, document);
            this.AddNextNteElement("Reference: " + panelSetOrder.AntiPlateletAntibodyIgGReference, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Test: Anti-Platelet Antibody - IgM", document);
            this.AddNextNteElement("Result: " + panelSetOrder.AntiPlateletAntibodyIgM, document);
            this.AddNextNteElement("Reference: " + panelSetOrder.AntiPlateletAntibodyIgMReference, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(panelSetOrder.ReticulatedPlateletAnalysis) == false)
            {
                this.AddNextNteElement("Test: Reticulated Platelet Analysis", document);
                this.AddNextNteElement("Result: " + panelSetOrder.ReticulatedPlateletAnalysis, document);
                this.AddNextNteElement("Reference Range: " + panelSetOrder.ReticulatedPlateletAnalysisReference, document);
                this.AddBlankNteElement(document);
            }

            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method", document);
            this.HandleLongString(panelSetOrder.Method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);
            this.AddBlankNteElement(document);

            this.HandleLongString(panelSetOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
