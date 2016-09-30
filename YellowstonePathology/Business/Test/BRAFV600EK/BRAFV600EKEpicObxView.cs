using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public BRAFV600EKEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            BRAFV600EKTestOrder panelSetOrder = (BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, "BRAF V600E Mutation Analysis");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Indication: " + panelSetOrder.Indication, document, "F");
            this.AddNextObxElement("", document, "F");
            
            this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.TumorNucleiPercentage) == false)
            {
                this.AddNextObxElement("Tumor Nuclei Percent: ", document, "F");
				this.HandleLongString(panelSetOrder.TumorNucleiPercentage, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string method = "Following lysis of paraffin embedded tissue; highly purified DNA was extracted from the specimen using an automated method.  PCR amplification using fluorescently labeled primers was then performed.  The products of the PCR reaction were then separated by high resolution capillary electrophoresis to look for the presence of the 107 nucleotide fragment indicative of a BRAF V600E mutation.";
            this.AddNextObxElement("Method: " + method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.AddNextObxElement(asr, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }		
	}
}
