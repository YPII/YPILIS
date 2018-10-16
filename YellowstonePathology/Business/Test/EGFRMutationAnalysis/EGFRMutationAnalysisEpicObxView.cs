using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
	public class EGFRMutationAnalysisEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public EGFRMutationAnalysisEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, egfrMutationAnalysisTestOrder, "EGFR Mutation Analysis");

            this.AddNextObxElement("", document, "F");            
            this.AddNextObxElement("Result:", document, "F");
            this.AddNextObxElement(egfrMutationAnalysisTestOrder.Result, document, "F");

            this.AddNextObxElement("", document, "F");
            if (string.IsNullOrEmpty(egfrMutationAnalysisTestOrder.ReferenceLabSignature) == false)
            {
                this.AddNextObxElement("Pathologist: " + egfrMutationAnalysisTestOrder.ReferenceLabSignature, document, "F");
                if (egfrMutationAnalysisTestOrder.FinalTime.HasValue == true)
                {
                    this.AddNextObxElement("E-signed " + egfrMutationAnalysisTestOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
                }
            }
            else
            {
                
                this.AddNextObxElement("Pathologist: " + egfrMutationAnalysisTestOrder.Signature, document, "F");
                if (egfrMutationAnalysisTestOrder.FinalTime.HasValue == true)
                {
                    this.AddNextObxElement("E-signed " + egfrMutationAnalysisTestOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
                }
            }
            
            
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Comment:", document, "F");
            this.HandleLongString(egfrMutationAnalysisTestOrder.Comment, document, "F"); 

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(egfrMutationAnalysisTestOrder.Interpretation, document, "F"); 

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Indication:", document, "F");
            this.HandleLongString(egfrMutationAnalysisTestOrder.Indication, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(egfrMutationAnalysisTestOrder.OrderedOn, egfrMutationAnalysisTestOrder.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");            

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Tumor Nuclei Percentage:", document, "F");
            this.AddNextObxElement(egfrMutationAnalysisTestOrder.TumorNucleiPercentage, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Microdissection Performed:", document, "F");
            string microdissectionPerformed = "No";
            if (egfrMutationAnalysisTestOrder.MicrodissectionPerformed == true) microdissectionPerformed = "Yes";
            this.AddNextObxElement(microdissectionPerformed, document, "F");            

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(egfrMutationAnalysisTestOrder.Method, document, "F");            

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(egfrMutationAnalysisTestOrder.ReportReferences, document, "F");

            this.AddNextObxElement("", document, "F");
            //string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            string asr = egfrMutationAnalysisTestOrder.ReportDisclaimer;
            this.HandleLongString(asr, document, "F");

            string locationPerformed = egfrMutationAnalysisTestOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
