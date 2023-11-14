using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationEPICNTEView : Business.HL7View.EPIC.EPICBeakerObxView
    {
        public LynchSyndromeEvaluationEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            //Add the first element as narrative for Nikki to see.
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement(panelSetOrderLynchSyndromeEvaluation.PanelSetName, document);

            this.AddNextNTEElement("Result:", document);
            this.AddNextNTEElement(panelSetOrderLynchSyndromeEvaluation.Result, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Interpretation:", document);
            this.AddNextNTEElement(panelSetOrderLynchSyndromeEvaluation.Interpretation, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Pathologist: " + panelSetOrderLynchSyndromeEvaluation.Signature, document);
            if (panelSetOrderLynchSyndromeEvaluation.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrderLynchSyndromeEvaluation.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement("", document);
            //this.AddAmendments(document);

            LynchSyndromeIHCPanelTest lynchSyndromeIHCPanelTest = new LynchSyndromeIHCPanelTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeIHCPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(lynchSyndromeIHCPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                this.AddNextNTEElement("Mismatch Repair Protein Expression by Immunohistochemistry: ", document);
                this.AddNextNTEElement("MLH1: " + panelSetOrderLynchSyndromeIHC.MLH1Result, document);
                this.AddNextNTEElement("PMS2: " + panelSetOrderLynchSyndromeIHC.PMS2Result, document);
                this.AddNextNTEElement("MSH2: " + panelSetOrderLynchSyndromeIHC.MSH2Result, document);
                this.AddNextNTEElement("MSH6: " + panelSetOrderLynchSyndromeIHC.MSH6Result, document);
                this.AddNextNTEElement("", document);
            }

            YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnalysis = new YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest();
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();

            if (((this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true ||
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true) &&
                panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth == true) ||
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                this.AddNextNTEElement("Molecular Analysis", document);
                this.AddNextNTEElement("", document);
            }

            if (panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth == true)
            {

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);

					this.AddNextNTEElement("BRAF V600E Mutation by PCR: " + panelSetOrderBraf.ReportNo, document);
					this.AddNextNTEElement("Result: " + panelSetOrderBraf.Result, document);
                    this.AddNextNTEElement("", document);   
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafMutationAnalysis.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnalysis.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);

                    this.AddNextNTEElement("BRAF Mutation Analsysis: " + panelSetOrderBraf.ReportNo, document);
                    this.AddNextNTEElement("Result: " + panelSetOrderBraf.Result, document);
                    this.AddNextNTEElement("", document);
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);

                    this.AddNextNTEElement("BRAF V600E Mutation by PCR: " + panelSetOrderRASRAF.ReportNo, document);
                    this.AddNextNTEElement("Result: " + panelSetOrderRASRAF.BRAFResult, document);
                    this.AddNextNTEElement("", document);
                }
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                this.AddNextNTEElement("MLH1 Methylation Analysis: " + panelSetOrderMLH1MethylationAnalysis.ReportNo, document);
                this.AddNextNTEElement("Result: " + panelSetOrderMLH1MethylationAnalysis.Result, document);
                this.AddNextNTEElement("", document);
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLynchSyndromeEvaluation.OrderedOn, panelSetOrderLynchSyndromeEvaluation.OrderedOnId);
            this.AddNextNTEElement("Specimen: " + specimenOrder.Description, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Method: ", document);
            this.AddNextNTEElement(panelSetOrderLynchSyndromeEvaluation.Method, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("References: ", document);
            this.AddNextNTEElement(panelSetOrderLynchSyndromeEvaluation.ReportReferences, document);
            this.AddNextNTEElement("", document);

            string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.AddNextNTEElement(asr, document);
            this.AddNextNTEElement("", document);

            LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndromeEvaluationTest();
            string peformedAtLocation = this.m_AccessionOrder.PanelSetOrderCollection.GetLocationPerformedSummary(lynchSyndromeEvaluationTest.PanelSetIDList);
            this.AddNextNTEElement(peformedAtLocation, document);
            this.AddNextNTEElement("", document);
        }
    }
}
