using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public LynchSyndromeEvaluationEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderLynchSyndromeEvaluation, "Lynch Syndrome Evaluation");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(panelSetOrderLynchSyndromeEvaluation.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString("Comment:", document, "F");
            this.AddNextObxElement(panelSetOrderLynchSyndromeEvaluation.Comment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrderLynchSyndromeEvaluation.Signature, document, "F");
            if (panelSetOrderLynchSyndromeEvaluation.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrderLynchSyndromeEvaluation.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            LynchSyndromeIHCPanelTest lynchSyndromeIHCPanelTest = new LynchSyndromeIHCPanelTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeIHCPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(lynchSyndromeIHCPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                this.AddNextObxElement("Mismatch Repair Protein Expression by Immunohistochemistry: ", document, "F");
                this.AddNextObxElement("MLH1: " + panelSetOrderLynchSyndromeIHC.MLH1Result, document, "F");
                this.AddNextObxElement("MSH2: " + panelSetOrderLynchSyndromeIHC.MSH2Result, document, "F");
                this.AddNextObxElement("MSH6: " + panelSetOrderLynchSyndromeIHC.MSH6Result, document, "F");
                this.AddNextObxElement("PMS2: " + panelSetOrderLynchSyndromeIHC.PMS2Result, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (((this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true ||
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true) &&
                panelSetOrderLynchSyndromeEvaluation.BRAFIsIndicated == true) ||
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                this.AddNextObxElement("Molecular Analysis", document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (panelSetOrderLynchSyndromeEvaluation.BRAFIsIndicated == true)
            {

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);

					this.AddNextObxElement("BRAF V600E Mutation by PCR: " + panelSetOrderBraf.ReportNo, document, "F");
					this.AddNextObxElement("Result: " + panelSetOrderBraf.Result, document, "F");
                    this.AddNextObxElement("", document, "F");   
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);

                    this.AddNextObxElement("BRAF V600E Mutation by PCR: " + panelSetOrderRASRAF.ReportNo, document, "F");
                    this.AddNextObxElement("Result: " + panelSetOrderRASRAF.BRAFResult, document, "F");
                    this.AddNextObxElement("", document, "F");
                }
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                this.AddNextObxElement("MLH1 Methylation Analysis: " + panelSetOrderMLH1MethylationAnalysis.ReportNo, document, "F");
                this.AddNextObxElement("Result: " + panelSetOrderMLH1MethylationAnalysis.Result, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLynchSyndromeEvaluation.OrderedOn, panelSetOrderLynchSyndromeEvaluation.OrderedOnId);
            this.AddNextObxElement("Specimen: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(panelSetOrderLynchSyndromeEvaluation.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrderLynchSyndromeEvaluation.References, document, "F");
            this.AddNextObxElement("", document, "F");

            string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.AddNextObxElement(asr, document, "F");
            this.AddNextObxElement("", document, "F");

            LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndromeEvaluationTest();
            string peformedAtLocation = this.m_AccessionOrder.PanelSetOrderCollection.GetLocationPerformedSummary(lynchSyndromeEvaluationTest.PanelSetIDList);
            this.HandleLongString(peformedAtLocation, document, "F");
            this.AddNextObxElement("", document, "F");
        }
    }
}
