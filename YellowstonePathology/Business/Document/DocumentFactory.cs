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
					case 1: //JAK2
						document = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					//case 2: //Cystic Fybrosis
					//	document = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument();
					//	break;
					case 3: //NGCT
                        document = new YellowstonePathology.Business.Test.NGCT.NGCTWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 12: //Future FISH                    
						document = new YellowstonePathology.Business.Document.HER2AmplificationReport(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 13: //Surgical
					case 128: //Non GYN Cytology
						document = new YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 14: //HPV
						document = new YellowstonePathology.Business.Test.HPV.HPVWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 15:  //Cytology                    
						document = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 18:  //BRAF
						document = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 19: //PNH
						document = new YellowstonePathology.Business.Test.PNH.PNHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 20: //Leukemia/Lymphoma Phenotyping
						document = new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 21: //Thrombocytopenia Profile
						document = new YellowstonePathology.Business.Test.ThombocytopeniaProfile.ThombocytopeniaProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 22: //Platelet Associated Antibodies
						document = new YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 23: //Reticulated platelet Analysis
						document = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 24: //Stem Cell Enumeration
						document = new YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 27: //New KRAS
						document = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 28: //Fetal Hemoglobin
						document = new YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 29: //DNA
						document = new YellowstonePathology.Business.Document.DnaReport(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 30: //KRAS Standard Reflex
						document = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 31: //TechnicalOnly
						document = new YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 32: //FactorVLeiden
						document = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 33: //Prothrombin
						document = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 35: //Autopsy
						document = new YellowstonePathology.Business.Test.Autopsy.AutopsyWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 36: //BCellClonality
						document = new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 46: //Her2ByIsh
						document = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 50: //ErPrSemiQuantitative
						document = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 60: //EGFR
						document = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 61: //Trichomonas
						document = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 62: //HPV 16/18
						document = new YellowstonePathology.Business.Test.HPV1618.HPV1618WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
                    case 213: //HPV 16/18
                        document = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 66:  //Test Cancelled
						document = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 100: //BCL1 t1114
						document = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 102:
						document = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 106:
						document = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 112:
						document = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 116:
						document = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 124:
						document = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 125:
						document = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 131:
						document = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 132:
						document = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 135:
						document = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 136:
						document = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 137:
						document = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 140: //Calreticulin V2
						document = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 141: //Jak2Exon1214 V2
						document = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 143: //ZAP 70 lymphoid panel
						document = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 144: //MLH1
						document = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 145: //Cytogenetic Analysis
						document = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 147: //Multiple Myeloma MGUS by FISH
						document = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 148: //CCNDI
						document = new YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 149: //High Grade Large B Cell Lymphoma
						document = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 150: // CEBPA
						document = new YellowstonePathology.Business.Test.CEBPA.CEBPAWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 151: // CLL by Fish
						document = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 152: // T Cell Clonality By PCR
						document = new YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement.TCellRecepterGammaGeneRearrangementWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 153: // FLT3
						document = new YellowstonePathology.Business.Test.FLT3.FLT3WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 155: // NPM1
						document = new YellowstonePathology.Business.Test.NPM1.NPM1WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 156: // BCRABL Fish
						document = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 157: // MPN Fish
						document = new YellowstonePathology.Business.Test.MPNFish.MPNFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 158: // MDS by Fish
						document = new YellowstonePathology.Business.Test.MDSByFish.MDSByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 159: // MPL
						document = new YellowstonePathology.Business.Test.MPL.MPLWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 160: // MultipleFISHProbePanel
						document = new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 161: // MultipleMyelomaIgHByFish
						document = new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 162: // BCRABLByPCR
						document = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 163: //Her2AmplificationByFish
						document = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 164: // MDS Extended Panel by Fish
						document = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 168: // AML Standard By Fish
						document = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 169: // Chromosome Analysis For Fetal Anomaly
						document = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 170: // Non Hodgkins Lymphoma FISH Panel
						document = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 171: // HER2 IHC
						document = new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 172: // Eosinophilia By FISH
						document = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 173: // Plasma Cell Myeloma Risk Stratification
						document = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 174: // NeoARRAY SNP Cytogenetic Profile
						document = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 175: // KRAS Exon 4 Mutation
						document = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 177: // B-Cell Gene Rearrangement
						document = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 178: // MYD88 Mutation Analysis
						document = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 179: // MYD88 Mutation Analysis
						document = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 181:  //CKIT
						document = new YellowstonePathology.Business.Test.CKIT.CKITWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 183: //Cystic Fybrosis
						document = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 184: //
						document = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 185: //
						document = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 186: //
						document = new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
					case 192: //
						document = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
						break;
                    case 81: //
                    case 165:
                    case 189:
                    case 190:                    
                    case 197:
                    case 201:
                    case 208:
                    case 211:
                    case 212:                    
                    case 230:            
                        document = new YellowstonePathology.Business.Document.NothingToPublishReport(accessionOrder, panelSetOrder, reportSaveMode);                    
                        break;
                    case 216:
                        document = new YellowstonePathology.Business.Test.InformalConsult.InformalConsultWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 203:
                        document = new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 204:
                        document = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 214:
                        document = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 215:
                        document = new YellowstonePathology.Business.Test.PDL1.PDL1WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 217:
                        document = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 218:
                        document = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 222:
                        document = new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 223:
                        document = new YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 225:
                        document = new YellowstonePathology.Business.Test.BCL2t1418ByPCR.BCL2t1418ByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 226:
                        document = new YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 227:
                        document = new YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR.CCNDIBCLIGHByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 228:
                        document = new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 229:
                        document = new YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 231:
                        document = new YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 232:
                        document = new YellowstonePathology.Business.Test.FGFR1.FGFR1WordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 233:
                        document = new YellowstonePathology.Business.Test.CSF3RMutationAnalysis.CSF3RMutationAnalysisWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    case 234:
                        document = new YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementWordDocument(accessionOrder, panelSetOrder, reportSaveMode);
                        break;
                    default:
						break;
				}
			}
			return document;
		}
	}
}
