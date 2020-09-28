using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
	public class EGFRMutationAnalysisEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public EGFRMutationAnalysisEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(egfrMutationAnalysisTestOrder.PanelSetId);

            //Add the first element as narrative for Nikki to see.
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.PanelSetName, document);

            this.AddNextNTEElement("", document);            
            this.AddNextNTEElement("Result:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.Result, document);

            this.AddNextNTEElement("", document);
            if (string.IsNullOrEmpty(egfrMutationAnalysisTestOrder.ReferenceLabSignature) == false)
            {
                this.AddNextNTEElement("Pathologist: " + egfrMutationAnalysisTestOrder.ReferenceLabSignature, document);
                if (egfrMutationAnalysisTestOrder.FinalTime.HasValue == true)
                {
                    this.AddNextNTEElement("E-signed " + egfrMutationAnalysisTestOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
                }
            }
            else
            {
                
                this.AddNextNTEElement("Pathologist: " + egfrMutationAnalysisTestOrder.Signature, document);
                if (egfrMutationAnalysisTestOrder.FinalTime.HasValue == true)
                {
                    this.AddNextNTEElement("E-signed " + egfrMutationAnalysisTestOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
                }
            }
            
            
            this.AddNextNTEElement("", document);
            this.AddAmendmentsNTE(document, egfrMutationAnalysisTestOrder, this.m_AccessionOrder);

            this.AddNextNTEElement("Comment:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.Comment, document); 

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Interpretation:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.Interpretation, document); 

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Indication:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.Indication, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Specimen Description:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(egfrMutationAnalysisTestOrder.OrderedOn, egfrMutationAnalysisTestOrder.OrderedOnId);
            this.AddNextNTEElement(specimenOrder.Description, document);            

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Tumor Nuclei Percentage:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.TumorNucleiPercentage, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Microdissection Performed:", document);
            string microdissectionPerformed = "No";
            if (egfrMutationAnalysisTestOrder.MicrodissectionPerformed == true) microdissectionPerformed = "Yes";
            this.AddNextNTEElement(microdissectionPerformed, document);            

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Method:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.Method, document);            

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("References:", document);
            this.AddNextNTEElement(egfrMutationAnalysisTestOrder.ReportReferences, document);

            this.AddNextNTEElement("", document);
            //string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            string asr = egfrMutationAnalysisTestOrder.ReportDisclaimer;
            this.AddNextNTEElement(asr, document);

            string locationPerformed = egfrMutationAnalysisTestOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }
    }
}
