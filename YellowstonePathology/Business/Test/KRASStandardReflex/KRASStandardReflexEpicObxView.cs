using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
	public class KRASStandardReflexEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public KRASStandardReflexEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
			KRASStandardReflexResult krasStandardReflexResult = KRASStandardReflexResultFactory.GetResult(this.m_ReportNo, this.m_AccessionOrder);

			this.AddHeader(document, krasStandardReflexResult.KRASStandardReflexTestOrder, "KRAS with BRAF reflex");
			this.AddNextObxElement("", document, "F");


            string krasResultString = "KRAS Result: " + krasStandardReflexResult.KRASStandardResult;
			if (krasStandardReflexResult.KRASStandardTestOrder.Result == "Detected")
			{
				krasResultString += " " + krasStandardReflexResult.KRASStandardTestOrder.MutationDetected;
			}
			this.AddNextObxElement(krasResultString, document, "F");

			this.AddNextObxElement("BRAF Result: " + krasStandardReflexResult.BRAFV600EKResult, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("Pathologist: " + krasStandardReflexResult.KRASStandardReflexTestOrder.Signature, document, "F");
			if (krasStandardReflexResult.KRASStandardReflexTestOrder.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + krasStandardReflexResult.KRASStandardReflexTestOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}
			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(krasStandardReflexResult.KRASStandardReflexTestOrder.OrderedOn, krasStandardReflexResult.KRASStandardReflexTestOrder.OrderedOnId);
			this.AddNextObxElement(specimenOrder.Description, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.HandleLongString("Comment: " + krasStandardReflexResult.KRASStandardReflexTestOrder.Comment, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Indication: " + krasStandardReflexResult.KRASStandardReflexTestOrder.Indication, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(krasStandardReflexResult.KRASStandardReflexTestOrder.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");

			if (krasStandardReflexResult.KRASStandardReflexTestOrder.TumorNucleiPercentage != null)
			{
				this.AddNextObxElement("Tumor Nuclei Percent: ", document, "F");
				this.HandleLongString(krasStandardReflexResult.KRASStandardReflexTestOrder.TumorNucleiPercentage, document, "F");
				this.AddNextObxElement("", document, "F");
			}

			this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(krasStandardReflexResult.KRASStandardReflexTestOrder.Method, document, "F");
			this.AddNextObxElement("", document, "F");
			
			this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(krasStandardReflexResult.KRASStandardReflexTestOrder.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");

			string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.HandleLongString(asr, document, "F");

            string locationPerformed = krasStandardReflexResult.KRASStandardReflexTestOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
    }
}
