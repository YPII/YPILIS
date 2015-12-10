using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class DocumentFactory
	{        
		public static YellowstonePathology.Business.Interface.ICaseDocument GetDocument(int panelSetId)
		{
            YellowstonePathology.Business.Interface.ICaseDocument document = null;

			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetId);
			if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
			{
				document = new ReferenceLabReport();
			}
			else if(panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument)
			{
				document = new DoNotPublishReport();
			}
			else
			{
				switch (panelSetId)
				{
					case 1: //JAK2
						document = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FWordDocument();
						break;
					//case 2: //Cystic Fybrosis
					//	document = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument();
					//	break;
					case 3: //NGCT
                        document = new YellowstonePathology.Business.Test.NGCT.NGCTWordDocument();
						break;
					case 12: //Future FISH                    
						document = new YellowstonePathology.Business.Document.HER2AmplificationReport();
						break;
					case 13: //Surgical
					case 128: //Non GYN Cytology
						document = new YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument();
						break;
					case 14: //HPV
						document = new YellowstonePathology.Business.Test.HPV.HPVWordDocument();
						break;
					case 15:  //Cytology                    
						document = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument();
						break;
					case 18:  //BRAF
						document = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKWordDocument();
						break;
					case 19: //PNH
						document = new YellowstonePathology.Business.Test.PNH.PNHWordDocument();
						break;
					case 20: //Leukemia/Lymphoma Phenotyping
						document = new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument();
						break;
					case 21: //Thrombocytopenia Profile
						document = new YellowstonePathology.Business.Test.ThombocytopeniaProfile.ThombocytopeniaProfileWordDocument();
						break;
					case 22: //Platelet Associated Antibodies
						document = new YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesWordDocument();
						break;
					case 23: //Reticulated platelet Analysis
						document = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisWordDocument();
						break;
					case 24: //Stem Cell Enumeration
						document = new YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationWordDocument();
						break;
					case 27: //New KRAS
						document = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardWordDocument();
						break;
					case 28: //Fetal Hemoglobin
						document = new YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinWordDocument();
						break;
					case 29: //DNA
						document = new YellowstonePathology.Business.Document.DnaReport();
						break;
					case 30: //KRAS Standard Reflex
						document = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument();
						break;
					case 31: //TechnicalOnly
						document = new YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyWordDocument();
						break;
					case 32: //FactorVLeiden
						document = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenWordDocument();
						break;
					case 33: //Prothrombin
						document = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinWordDocument();
						break;
					case 35: //Autopsy
						document = new YellowstonePathology.Business.Test.Autopsy.AutopsyWordDocument();
						break;
					case 36: //BCellClonality
						document = new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCRWordDocument();
						break;
					case 46: //Her2ByIsh
						document = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument();
						break;
					case 50: //ErPrSemiQuantitative
						document = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeWordDocument();
						break;
					case 60: //EGFR
						document = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument();
						break;
					case 61: //Trichomonas
						document = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasWordDocument();
						break;
					case 62: //HPV 16/18
						document = new YellowstonePathology.Business.Test.HPV1618.HPV1618WordDocument();
						break;
                    case 213: //HPV 16/18
                        document = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRWordDocument();
                        break;
                    case 66:  //Test Cancelled
						document = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledWordDocument();
						break;
					case 100: //BCL1 t1114
						document = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114WordDocument();
						break;
					case 102:
						document = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelWordDocument();
						break;
					case 106:
						document = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationWordDocument();
						break;
					case 112:
						document = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWordDocument();
						break;
					case 116:
						document = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWordDocument();
						break;
					case 124:
						document = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument();
						break;
					case 125:
						document = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelWordDocument();
						break;
					case 131:
						document = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHWordDocument();
						break;
					case 132:
						document = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisWordDocument();
						break;
					case 135:
						document = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationWordDocument();
						break;
					case 136:
						document = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument();
						break;
					case 137:
						document = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexWordDocument();
						break;
					case 140: //Calreticulin V2
						document = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisWordDocument();
						break;
					case 141: //Jak2Exon1214 V2
						document = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214WordDocument();
						break;
					case 143: //ZAP 70 lymphoid panel
						document = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelWordDocument();
						break;
					case 144: //MLH1
						document = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisWordDocument();
						break;
					case 145: //Cytogenetic Analysis
						document = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisWordDocument();
						break;
					case 147: //Multiple Myeloma MGUS by FISH
						document = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishWordDocument();
						break;
					case 148: //CCNDI
						document = new YellowstonePathology.Business.Test.CCNDIBCLIGH.CCNDIBCLIGHWordDocument();
						break;
					case 149: //High Grade Large B Cell Lymphoma
						document = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaWordDocument();
						break;
					case 150: // CEBPA
						document = new YellowstonePathology.Business.Test.CEBPA.CEBPAWordDocument();
						break;
					case 151: // CLL by Fish
						document = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishWordDocument();
						break;
					case 152: // T Cell Clonality By PCR
						document = new YellowstonePathology.Business.Test.TCellClonalityByPCR.TCellClonalityByPCRWordDocument();
						break;
					case 153: // FLT3
						document = new YellowstonePathology.Business.Test.FLT3.FLT3WordDocument();
						break;
					case 155: // NPM1
						document = new YellowstonePathology.Business.Test.NPM1.NPM1WordDocument();
						break;
					case 156: // BCRABL Fish
						document = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishWordDocument();
						break;
					case 157: // MPN Fish
						document = new YellowstonePathology.Business.Test.MPNFish.MPNFishWordDocument();
						break;
					case 158: // MDS by Fish
						document = new YellowstonePathology.Business.Test.MDSByFish.MDSByFishWordDocument();
						break;
					case 159: // MPL
						document = new YellowstonePathology.Business.Test.MPL.MPLWordDocument();
						break;
					case 160: // MultipleFISHProbePanel
						document = new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeWordDocument();
						break;
					case 161: // MultipleMyelomaIgHByFish
						document = new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishWordDocument();
						break;
					case 162: // BCRABLByPCR
						document = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCRWordDocument();
						break;
					case 163: //Her2AmplificationByFish
						document = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishWordDocument();
						break;
					case 164: // MDS Extended Panel by Fish
						document = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishWordDocument();
						break;
					case 168: // AML Standard By Fish
						document = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishWordDocument();
						break;
					case 169: // Chromosome Analysis For Fetal Anomaly
						document = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyWordDocument();
						break;
					case 170: // Non Hodgkins Lymphoma FISH Panel
						document = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelWordDocument();
						break;
					case 171: // HER2 IHC
						document = new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument();
						break;
					case 172: // Eosinophilia By FISH
						document = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHWordDocument();
						break;
					case 173: // Plasma Cell Myeloma Risk Stratification
						document = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationWordDocument();
						break;
					case 174: // NeoARRAY SNP Cytogenetic Profile
						document = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileWordDocument();
						break;
					case 175: // KRAS Exon 4 Mutation
						document = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationWordDocument();
						break;
					case 177: // B-Cell Gene Rearrangement
						document = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementWordDocument();
						break;
					case 178: // MYD88 Mutation Analysis
						document = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisWordDocument();
						break;
					case 179: // MYD88 Mutation Analysis
						document = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisWordDocument();
						break;
					case 181:  //CKIT
						document = new YellowstonePathology.Business.Test.CKIT.CKITWordDocument();
						break;
					case 183: //Cystic Fybrosis
						document = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument();
						break;
					case 184: //
						document = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qWordDocument();
						break;
					case 185: //
						document = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionWordDocument();
						break;
					case 186: //
						document = new YellowstonePathology.Business.Test.API2MALT1.API2MALT1WordDocument();
						break;
					case 192: //
						document = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHWordDocument();
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
                    case 216:            
                        document = new YellowstonePathology.Business.Document.NothingToPublishReport();                    
                        break;
                    
                    case 203:
                        document = new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument();
                        break;
                    case 204:
                        document = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument();
                        break;
                    case 214:
                        document = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearWordDocument();
                        break;
                    case 215:
                        document = new YellowstonePathology.Business.Test.PDL1.PDL1WordDocument();
                        break;
                    case 217:
                        document = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationWordDocument();
                        break;
                    case 218:
                        document = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelWordDocument();
                        break;
                    default:
						break;
				}
			}
			return document;
		}
	}
}
