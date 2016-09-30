using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class DocumentFactory
	{
        public static YellowstonePathology.Business.Interface.ICaseDocument GetDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, ReportSaveModeEnum reportSaveMode)
        {
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
            Type caseDocumentType = Type.GetType(panelSet.WordDocumentClassName);
            YellowstonePathology.Business.Interface.ICaseDocument document = (YellowstonePathology.Business.Interface.ICaseDocument)Activator.CreateInstance(caseDocumentType, accessionOrder, panelSetOrder, reportSaveMode);
            return document;
        }

        /*public static YellowstonePathology.Business.Interface.ICaseDocument GetDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, ReportSaveModeEnum reportSaveMode)
		{
            YellowstonePathology.Business.Interface.ICaseDocument document = null;

			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
			if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
			{
				document = new ReferenceLabReport(accessionOrder, panelSetOrder, reportSaveMode);
			}
			else if(panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument)
			{
				document = new DoNotPublishReport(accessionOrder, panelSetOrder, reportSaveMode);
			}
			else
			{
				switch (panelSetOrder.PanelSetId)
				{
					case 1: //JAK2 x
						document = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					//case 2: //Cystic Fybrosis
					//	document = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument();
					//	break;
					case 3: //NGCT x
                        document = new YellowstonePathology.Business.Test.NGCT.NGCTWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 12: //Future FISH          ???       x   
						document = new YellowstonePathology.Business.Document.HER2AmplificationReport(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 13: //Surgical x
					case 128: //Non GYN Cytology x
						document = new YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 14: //HPV x
						document = new YellowstonePathology.Business.Test.HPV.HPVWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 15:  //Cytology   x                 
						document = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 18:  //BRAF x
						document = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 19: //PNH x
						document = new YellowstonePathology.Business.Test.PNH.PNHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 20: //Leukemia/Lymphoma Phenotyping x
						document = new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 21: //Thrombocytopenia Profile x
						document = new YellowstonePathology.Business.Test.ThombocytopeniaProfile.ThombocytopeniaProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 22: //Platelet Associated Antibodies x
						document = new YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 23: //Reticulated platelet Analysis x
						document = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 24: //Stem Cell Enumeration x
						document = new YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 27: //New KRAS x
						document = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 28: //Fetal Hemoglobin x
						document = new YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 29: //DNA ??? no PS
						document = new YellowstonePathology.Business.Document.DnaReport(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 30: //KRAS Standard Reflex x
						document = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 31: //TechnicalOnly ??? x
						document = new YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 32: //FactorVLeiden x
						document = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 33: //Prothrombin x
						document = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 35: //Autopsy ??? source is pub doc but has rpt x
						document = new YellowstonePathology.Business.Test.Autopsy.AutopsyWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 36: //BCellClonality x
						document = new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 46: //Her2ByIsh x
						document = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 50: //ErPrSemiQuantitative x
						document = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 60: //EGFR x
						document = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 61: //Trichomonas x
						document = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 62: //HPV 16/18 x
						document = new YellowstonePathology.Business.Test.HPV1618.HPV1618WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
                    case 213: //HPV 16/18 x
                        document = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 66:  //Test Cancelled x
						document = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 100: //BCL1 t1114 x
						document = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 102: //x
						document = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 106: //x
						document = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 112: //x
						document = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 116: //x
						document = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 124: //x
						document = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 125: //x
						document = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 131: //x
						document = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 132: //x
						document = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 135: //x
						document = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 136: //x
						document = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 137: //x
						document = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 140: //Calreticulin V2 x
						document = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 141: //Jak2Exon1214 V2 x
						document = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 143: //ZAP 70 lymphoid panel x
						document = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 144: //MLH1 x
						document = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 145: //Cytogenetic Analysis x
						document = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 147: //Multiple Myeloma MGUS by FISH x
						document = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 148: //CCNDI x
						document = new YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 149: //High Grade Large B Cell Lymphoma x
						document = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 150: // CEBPA x
						document = new YellowstonePathology.Business.Test.CEBPA.CEBPAWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 151: // CLL by Fish x
						document = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 152: // T Cell Clonality By PCR x
						document = new YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement.TCellRecepterGammaGeneRearrangementWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 153: // FLT3 x
						document = new YellowstonePathology.Business.Test.FLT3.FLT3WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 155: // NPM1 x
						document = new YellowstonePathology.Business.Test.NPM1.NPM1WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 156: // BCRABL Fish x
						document = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 157: // MPN Fish x
						document = new YellowstonePathology.Business.Test.MPNFish.MPNFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 158: // MDS by Fish x
						document = new YellowstonePathology.Business.Test.MDSByFish.MDSByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 159: // MPL x
						document = new YellowstonePathology.Business.Test.MPL.MPLWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 160: // MultipleFISHProbePanel x
						document = new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 161: // MultipleMyelomaIgHByFish x
						document = new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 162: // BCRABLByPCR x
						document = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 163: //Her2AmplificationByFish x
						document = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 164: // MDS Extended Panel by Fish x
						document = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 168: // AML Standard By Fish x
						document = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 169: // Chromosome Analysis For Fetal Anomaly x
						document = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 170: // Non Hodgkins Lymphoma FISH Panel x
						document = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 171: // HER2 IHC x
						document = new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 172: // Eosinophilia By FISH x
						document = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 173: // Plasma Cell Myeloma Risk Stratification x
						document = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 174: // NeoARRAY SNP Cytogenetic Profile x
						document = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 175: // KRAS Exon 4 Mutation x
						document = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 177: // B-Cell Gene Rearrangement x
						document = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 178: // MYD88 Mutation Analysis x
						document = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 179: // x
						document = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 181:  //CKIT x
						document = new YellowstonePathology.Business.Test.CKIT.CKITWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 183: //Cystic Fybrosis x
						document = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 184: //x
						document = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 185: //x
						document = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 186: //x
						document = new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 192: //x
						document = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
                    case 81: // base 81??? 2 w/ same id??? source is db no doc, source is db doc is FNAAdequacyAssessmentTestOrder x
                    case 165:
                    case 189:
                    case 190:                    
                    case 197: //??? source is db no rpt x
                    case 201: //??? source is db no rpt x
                    case 208:
                    case 212: //??? source is db no rpt x
                    case 230:            
                        document = new YellowstonePathology.Business.Document.NothingToPublishReport(accessionOrder, panelSetOrder, reportSaveMode);                    
                        break;
                    case 216: //??? source is none, doc is hold for flow x
                        document = new YellowstonePathology.Business.Test.InformalConsult.InformalConsultWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 203: //x
                        document = new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 204: //x
                        document = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 211: //??? source is none, doc is hold for flow x
                        document = new YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 214: //x
                        document = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 215: //x
                        document = new YellowstonePathology.Business.Test.PDL1.PDL1WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 217: //x
                        document = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 218: //x
                        document = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 222: //x
                        document = new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 223: //x
                        document = new YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 225: //x
                        document = new YellowstonePathology.Business.Test.BCL2t1418ByPCR.BCL2t1418ByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 226: //x
                        document = new YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 227: //x
                        document = new YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR.CCNDIBCLIGHByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 228: //x
                        document = new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 229: //x
                        document = new YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 231: //x
                        document = new YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 232: //x
                        document = new YellowstonePathology.Business.Test.FGFR1.FGFR1WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 233: //x
                        document = new YellowstonePathology.Business.Test.CSF3RMutationAnalysis.CSF3RMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 234: //x
                        document = new YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 235: //??? wrong doc sb YellowstonePathology.Business.Document.ReferenceLabReport x
                        document = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    default:
						break;
                        //166??? not in factory but in PSCol source db no rpt
                        //167??? not in factory but in PSCol source db no rpt
                }
            }
			return document;
		}*/
	}
}
