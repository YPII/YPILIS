using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public ComprehensiveColonCancerProfileEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {			            
			ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult = new ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, comprehensiveColonCancerProfile);

            this.AddHeader(document, comprehensiveColonCancerProfile, "Comprehensive Colon Cancer Profile");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(comprehensiveColonCancerProfile.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + comprehensiveColonCancerProfile.Signature, document, "F");
            if (comprehensiveColonCancerProfile.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + comprehensiveColonCancerProfile.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            foreach(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in comprehensiveColonCancerProfileResult.SurgicalSpecimenCollection)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(surgicalSpecimen.SpecimenOrderId);
                StringBuilder specimenDescription = new StringBuilder();
                specimenDescription.Append(specimenOrder.Description + ": ");
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    specimenDescription.Append(aliquotOrder.Label + ", ");
                }
                specimenDescription.Remove(specimenDescription.Length - 2, 2);
                this.AddNextObxElement("Specimen:", document, "F");
                this.HandleLongString(specimenDescription.ToString(), document, "F");
                this.AddNextObxElement("", document, "F");

                this.HandleLongString("Diagnosis:", document, "F");
                this.HandleLongString(surgicalSpecimen.Diagnosis, document, "F");
                this.AddNextObxElement("", document, "F");
            }

			this.HandleLongString("AJCC Pathologic Staging:", document, "F");
            this.HandleLongString(comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.AJCCStage, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Reference Report: " + comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.ReportNo, document, "F");
			this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Mismatch Repair Protein Expression by Immunohistochemistry: ", document, "F");
            if (comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHCCollection.Count > 0)
            {
                foreach (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC testOrder in comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHCCollection)
                {
                    this.AddNextObxElement("MLH1: " + testOrder.MLH1Result, document, "F");
                    this.AddNextObxElement("MSH2: " + testOrder.MSH2Result, document, "F");
                    this.AddNextObxElement("MSH6: " + testOrder.MSH6Result, document, "F");
                    this.AddNextObxElement("PMS2: " + testOrder.PMS2Result, document, "F");
                    this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                    this.AddNextObxElement("", document, "F");
                }
            }
            else
            {
                this.AddNextObxElement("MLH1: " + "Not Included", document, "F");
                this.AddNextObxElement("MSH2: " + "Not Included", document, "F");
                this.AddNextObxElement("MSH6: " + "Not Included", document, "F");
                this.AddNextObxElement("PMS2: " + "Not Included", document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Molecular Analysis", document, "F");
            if (comprehensiveColonCancerProfileResult.MolecularTestOrderCollection.Count > 0)
            {

                foreach (YellowstonePathology.Business.Test.PanelSetOrder testOrder in comprehensiveColonCancerProfileResult.MolecularTestOrderCollection)
                {
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimen = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(testOrder.OrderedOnId);
                    YellowstonePathology.Business.Test.AliquotOrder aliquot = specimen.AliquotOrderCollection.GetByAliquotOrderId(testOrder.OrderedOnId);
                    if (testOrder is LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)
                    {
                        this.AddNextObxElement("MLH1 Promoter Methylation Analysis: " + ((LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)testOrder).Result, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");

                    }
                    else if (testOrder is KRASStandard.KRASStandardTestOrder)
                    {
                        this.AddNextObxElement("KRAS Mutation Analysis: " + ((KRASStandard.KRASStandardTestOrder)testOrder).Result, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");
                    }
                    else if (testOrder is KRASExon23Mutation.KRASExon23MutationTestOrder)
                    {
                        this.AddNextObxElement("KRAS Exon 23 Mutation Analysis: " + ((KRASExon23Mutation.KRASExon23MutationTestOrder)testOrder).Result, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");
                    }
                    else if (testOrder is KRASExon4Mutation.KRASExon4MutationTestOrder)
                    {
                        this.AddNextObxElement("KRAS Exon 4 Mutation Analysis: " + ((KRASExon4Mutation.KRASExon4MutationTestOrder)testOrder).Result, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");
                    }
                    else if (testOrder is BRAFV600EK.BRAFV600EKTestOrder)
                    {
                        this.AddNextObxElement("BRAF V600E Mutation Analysis: " + ((BRAFV600EK.BRAFV600EKTestOrder)testOrder).Result, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");
                    }
                    else if (testOrder is NRASMutationAnalysis.NRASMutationAnalysisTestOrder)
                    {
                        this.AddNextObxElement("NRAS Mutation Analysis: " + ((NRASMutationAnalysis.NRASMutationAnalysisTestOrder)testOrder).Result, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");
                    }
                    else if (testOrder is RASRAFPanel.RASRAFPanelTestOrder)
                    {
                        this.AddNextObxElement("KRAS Mutation Analysis: " + ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).KRASResult, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");

                        this.AddNextObxElement("BRAF V600E/K Mutation Analysis: " + ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).BRAFResult, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");

                        this.AddNextObxElement("NRAS Mutation Analysis: " + ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).NRASResult, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");

                        this.AddNextObxElement("HRAS Mutation Analysis: " + ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).HRASResult, document, "F");
                        this.AddNextObxElement("Specimen: " + aliquot.Label, document, "F");
                        this.AddNextObxElement("Reference Report: " + testOrder.ReportNo, document, "F");
                        this.AddNextObxElement("", document, "F");
                    }
                }
            }
            else
            {                        
                this.AddNextObxElement("None Performed", document, "F");
                this.AddNextObxElement("", document, "F");
            }
        }
    }
}
