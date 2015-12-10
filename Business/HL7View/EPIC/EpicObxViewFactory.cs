using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EpicObxViewFactory
    {
        public static EpicObxView GetObxView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            EpicObxView view = null;
            switch (panelSetId)
            {
                case 1:
					view = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FEpicObxView(accessionOrder, reportNo, obxCount);
                    break;                
                case 3:
                    view = new YellowstonePathology.Business.Test.NGCT.NGCTEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 46:
					view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 13:
                case 128:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 14:
					view = new YellowstonePathology.Business.Test.HPV.HPVEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 15:
					view = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 18:
					view = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 19:
					view = new YellowstonePathology.Business.Test.PNH.PNHEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 27:
					view = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 20:
					view = new YellowstonePathology.Business.Test.LLP.LLPEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 21:
					view = new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 23:
					view = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.RPAEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 30:
					view = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 32:
					view = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 33:
					view = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 50:
					view = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeEpicObxView(accessionOrder, reportNo, obxCount);
					break;
                case 60:
					view = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 61:
					view = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 62:
					view = new YellowstonePathology.Business.Test.HPV1618.HPV1618EpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 213:
                    view = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCREPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 66:
					view = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 100:
					view = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 102:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 106:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationEpicObxView(accessionOrder, reportNo, obxCount);
                    break;  
				case 112:
					view = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileEpicObxView(accessionOrder, reportNo, obxCount);
					break;
                case 116:
					view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 124:
					view = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisEPICOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 131:
                    view = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 132:
					view = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 135:
					view = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationEpicObxView(accessionOrder, reportNo, obxCount);
					break;
                case 136:
					view = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 137:
					view = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 140:
					view = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 141:
					view = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 143:
					view = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 144:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 145:
					view = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 147:
					view = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 148:
					view = new YellowstonePathology.Business.Test.CCNDIBCLIGH.CCNDIBCLIGHEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 149:
					view = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 150:
					view = new YellowstonePathology.Business.Test.CEBPA.CEBPAEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 151:
					view = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 152:
					view = new YellowstonePathology.Business.Test.TCellClonalityByPCR.TCellClonalityByPCREpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 153:
					view = new YellowstonePathology.Business.Test.FLT3.FLT3EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 155:
					view = new YellowstonePathology.Business.Test.NPM1.NPM1EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 156:
					view = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 157:
					view = new YellowstonePathology.Business.Test.MPNFish.MPNFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 158:
					view = new YellowstonePathology.Business.Test.MDSByFish.MDSByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 159:
					view = new YellowstonePathology.Business.Test.MPL.MPLEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 160:
					view = new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 161:
					view = new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 162:
					view = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCREpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 163:
					view = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 164:
					view = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 168:
					view = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 169:
					view = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 170:
					view = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 171:
					view = new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 172:
					view = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 173:
					view = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 174:
					view = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 175:
					view = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 177:
					view = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 178:
					view = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 179:
					view = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 181:
					view = new YellowstonePathology.Business.Test.CKIT.CKITEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 183:
					view = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 184:
					view = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 185:
					view = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 186:
					view = new YellowstonePathology.Business.Test.API2MALT1.API2MALT1EpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 192:
					view = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHEpicObxView(accessionOrder, reportNo, obxCount);
					break;
                case 204:
                    view = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 214:
                    view = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                //case 215:
                //    view = new YellowstonePathology.Business.Test.PDL1.PDL1EpicObxView(accessionOrder, reportNo, obxCount);
                //    break;
                case 217:
                    view = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 218:
                    view = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelEpicObxView(accessionOrder, reportNo, obxCount);
                    break;
            }
            return view;
        }
    }
}
