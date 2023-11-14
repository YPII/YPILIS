using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfile
{
    public class ThrombocytopeniaProfileCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public ThrombocytopeniaProfileCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Flow.FlowMarkerItem iggMarker = panelSetOrder.FlowMarkerCollection.GetMarkerByName("Anti-Platelet Antibody - IgG");
            this.AddNextNteElement("Test: " + iggMarker.Name, document);
            this.AddNextNteElement("Result: " + iggMarker.Result, document);
            this.AddNextNteElement("Reference: Negative", document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Flow.FlowMarkerItem igMMarker = panelSetOrder.FlowMarkerCollection.GetMarkerByName("Anti-Platelet Antibody - IgM");
            this.AddNextNteElement("Test: " + igMMarker.Name, document);
            this.AddNextNteElement("Result: " + igMMarker.Result, document);
            this.AddNextNteElement("Reference: Negative", document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Flow.FlowMarkerItem rpaMarker = panelSetOrder.FlowMarkerCollection.GetMarkerByName("Reticulated Platelet Analysis");
            this.AddNextNteElement("Test: " + rpaMarker.Name, document);
            this.AddNextNteElement("Result: " + rpaMarker.Result, document);
            this.AddNextNteElement("Reference Range: 0-0.37%", document);
            this.AddBlankNteElement(document);

            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method", document);
            this.AddNextNteElement("Reticulated Platelets: Quantitative Flow Cytometry", document);
            this.AddNextNteElement("Platelet Associated Antibodies: Qualitative Flow Cytometry", document);
            this.AddBlankNteElement(document);

            string interpretiveComment = "Negative:  IgG and/or IgM values are not elevated. There is no indication that immune mechanisms are involved in the thrombocytopenia. Other etiologies should be considered. Weakly Positive:  The moderately elevated IgG and/or IgM value suggests that immune mechanisms could be involved in the thrombocytopenia.  Other etiologies should also be considered. Positive: The elevated IgG and/or IgM value suggests that immune mechanisms are involved in the thrombocytopenia. Strongly Positive:  The IgG and/or IgM value is greatly elevated and indicates that immune mechanisms are involved in the thrombocytopenia.";
            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(interpretiveComment, document);
            this.AddBlankNteElement(document);

            string disclaimer = "Tests utilizing Analytic Specific Reagents (ASRs) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.   This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.HandleLongString(disclaimer, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
