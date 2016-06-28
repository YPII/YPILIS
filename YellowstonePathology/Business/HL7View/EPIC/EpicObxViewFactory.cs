using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICObxViewFactory
    {
        public static EPICObxView GetObxView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            EPICObxView view = null;
            switch (panelSetId)
            {
                case 1:
					view = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FEPICObxView(accessionOrder, reportNo, obxCount);
                    break;                
                case 3:
                    view = new YellowstonePathology.Business.Test.NGCT.NGCTEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 46:
					view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 13:
                case 128:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 14:
					view = new YellowstonePathology.Business.Test.HPV.HPVEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 15:
					view = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 18:
					view = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 19:
					view = new YellowstonePathology.Business.Test.PNH.PNHEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 27:
					view = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 20:
					view = new YellowstonePathology.Business.Test.LLP.LLPEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 21:
					view = new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 23:
					view = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.RPAEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 30:
					view = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 32:
					view = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 33:
					view = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 50:
					view = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeEPICObxView(accessionOrder, reportNo, obxCount);
					break;
                case 60:
					view = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 61:
					view = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 62:
					view = new YellowstonePathology.Business.Test.HPV1618.HPV1618EPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 213:
                    view = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCREPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 66:
					view = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 100:
					view = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114EPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 102:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 106:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationEPICObxView(accessionOrder, reportNo, obxCount);
                    break;  
				case 112:
					view = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileEPICObxView(accessionOrder, reportNo, obxCount);
					break;
                case 116:
					view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 124:
					view = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisEPICOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 131:
                    view = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 132:
					view = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 135:
					view = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationEPICObxView(accessionOrder, reportNo, obxCount);
					break;
                case 136:
					view = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
				case 137:
					view = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 140:
					view = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 141:
					view = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214EPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 143:
					view = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 144:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 145:
					view = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 147:
					view = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 148:
					view = new YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 149:
					view = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 150:
					view = new YellowstonePathology.Business.Test.CEBPA.CEBPAEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 151:
					view = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 152:
					view = new YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement.TCellRecepterGammaGeneRearrangementEPICOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 153:
					view = new YellowstonePathology.Business.Test.FLT3.FLT3EPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 155:
					view = new YellowstonePathology.Business.Test.NPM1.NPM1EPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 156:
					view = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 157:
					view = new YellowstonePathology.Business.Test.MPNFish.MPNFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 158:
					view = new YellowstonePathology.Business.Test.MDSByFish.MDSByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 159:
					view = new YellowstonePathology.Business.Test.MPL.MPLEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 160:
					view = new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 161:
					view = new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 162:
					view = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCREPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 163:
					view = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 164:
					view = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 168:
					view = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 169:
					view = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 170:
					view = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 171:
					view = new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 172:
					view = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 173:
					view = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 174:
					view = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 175:
					view = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 177:
					view = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementEpicObxView(accessionOrder, reportNo, obxCount);
					break;
				case 178:
					view = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 179:
					view = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 181:
					view = new YellowstonePathology.Business.Test.CKIT.CKITEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 183:
					view = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 184:
					view = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 185:
					view = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 186:
					view = new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHEPICObxView(accessionOrder, reportNo, obxCount);
					break;
				case 192:
					view = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHEPICObxView(accessionOrder, reportNo, obxCount);
					break;
                case 204:
                    view = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 214:
                    view = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 215:
                    view = new YellowstonePathology.Business.Test.PDL1.PDL1EPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 217:
                    view = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 218:
                    view = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 222:
                    view = new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 223:
                    view = new YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 225:
                    view = new YellowstonePathology.Business.Test.BCL2t1418ByPCR.BCL2t1418ByPCREPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 226:
                    view = new YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHEPICObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 227:
                    view = new YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR.CCNDIBCLIGHByPCREPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 228:
                    view = new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCREPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 229:
                    view = new YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 231:
                    view = new YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 232:
                    view = new YellowstonePathology.Business.Test.FGFR1.FGFR1EPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 233:
                    view = new YellowstonePathology.Business.Test.CSF3RMutationAnalysis.CSF3RMutationAnalysisEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 234:
                    view = new YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementEPICOBXView(accessionOrder, reportNo, obxCount);
                    break;
            }
            return view;
        }
    }
}
