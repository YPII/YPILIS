using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfile
{
	public class ThrombocytopeniaProfileEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public ThrombocytopeniaProfileEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, "Thrombocytopenia Profile");
            this.AddNextObxElement("", document, "F");
            
            YellowstonePathology.Business.Flow.FlowMarkerItem iggMarker = panelSetOrder.FlowMarkerCollection.GetMarkerByName("Anti-Platelet Antibody - IgG");
            this.AddNextObxElement("Test: " + iggMarker.Name, document, "F");
            this.AddNextObxElement("Result: " + iggMarker.Result, document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            YellowstonePathology.Business.Flow.FlowMarkerItem igMMarker = panelSetOrder.FlowMarkerCollection.GetMarkerByName("Anti-Platelet Antibody - IgM");
            this.AddNextObxElement("Test: " + igMMarker.Name, document, "F");
            this.AddNextObxElement("Result: " + igMMarker.Result, document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            YellowstonePathology.Business.Flow.FlowMarkerItem rpaMarker = panelSetOrder.FlowMarkerCollection.GetMarkerByName("Reticulated Platelet Analysis");
            this.AddNextObxElement("Test: " + rpaMarker.Name, document, "F");
            this.AddNextObxElement("Result: " + rpaMarker.Result, document, "F");
            this.AddNextObxElement("Reference Range: 0-0.37%", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method", document, "F");
            this.AddNextObxElement("Reticulated Platelets: Quantitative Flow Cytometry", document, "F");
            this.AddNextObxElement("Platelet Associated Antibodies: Qualitative Flow Cytometry", document, "F");
            this.AddNextObxElement("", document, "F");

            string interpretiveComment = "Negative:  IgG and/or IgM values are not elevated. There is no indication that immune mechanisms are involved in the thrombocytopenia. Other etiologies should be considered. Weakly Positive:  The moderately elevated IgG and/or IgM value suggests that immune mechanisms could be involved in the thrombocytopenia.  Other etiologies should also be considered. Positive: The elevated IgG and/or IgM value suggests that immune mechanisms are involved in the thrombocytopenia. Strongly Positive:  The IgG and/or IgM value is greatly elevated and indicates that immune mechanisms are involved in the thrombocytopenia.";
            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(interpretiveComment, document, "F");
            this.AddNextObxElement("", document, "F");

            string disclaimer = "Tests utilizing Analytic Specific Reagents (ASRs) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.   This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.HandleLongString(disclaimer, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
