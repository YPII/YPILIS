using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis
{
	public class RPAEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public RPAEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            System.Diagnostics.Trace.Assert(panelSetOrder.FlowMarkerCollection.Count == 1, "There can only be one marker for this test type.");

            this.AddHeader(document, panelSetOrder, "Reticulated Platelet Analysis");
            this.AddNextObxElement(string.Empty, document, "F");

            string result = panelSetOrder.FlowMarkerCollection[0].Result;
            this.AddNextObxElement("Result: " + result, document, "F");
            this.AddNextObxElement("Reference: 0 - 0.37%", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddAmendments(document);

            this.AddNextObxElement("Antibodies Used: CD41, Thiozole Orange", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: Quantitative Flow Cytometry", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Tests utilizing Analytic Specific Reagents (ASR’s) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR’s may be used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
