using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.WPH
{
    public class WPHOBXViewFactory
    {
        public static WPHOBXView GetObxView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            WPHOBXView view = null;
            switch (panelSetId)
            {
                case 1:
					view = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;                
                case 3:
                    //view = new YellowstonePathology.Business.Test.NGCT.NGCTWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 46:
					//view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 13:
                case 128:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalWPHObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 14:
					//view = new YellowstonePathology.Business.Test.HPV.HPVWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 15:
					//view = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 18:
					//view = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 19:
					//view = new YellowstonePathology.Business.Test.PNH.PNHWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 27:
					//view = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 20:
					//view = new YellowstonePathology.Business.Test.LLP.LLPWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 21:
					//view = new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 23:
					//view = new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.RPAWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 30:
					//view = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 32:
					//view = new YellowstonePathology.Business.Test.FactorVLeiden.FactorVWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 33:
					//view = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 50:
					//view = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 60:
					//view = new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 61:
					//view = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 62:
					//view = new YellowstonePathology.Business.Test.HPV1618.HPV1618WPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 213:
                    //view = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 66:
					//view = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 100:
					//view = new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114WPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 102:
					//view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 106:
					//view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;  
				case 112:
					//view = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 116:
					//view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 124:
					//view = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 131:
                    //view = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 132:
					//view = new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 135:
					//view = new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 136:
					//view = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
				case 137:
					//view = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 140:
					//view = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 141:
					//view = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214WPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 143:
					//view = new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 144:
					//view = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 145:
					//view = new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 147:
					//view = new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 148:
					//view = new YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 149:
					//view = new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 150:
					//view = new YellowstonePathology.Business.Test.CEBPA.CEBPAWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 151:
					//view = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 152:
					//view = new YellowstonePathology.Business.Test.TCellClonalityByPCR.TCellClonalityByPCRWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 153:
					//view = new YellowstonePathology.Business.Test.FLT3.FLT3WPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 155:
					//view = new YellowstonePathology.Business.Test.NPM1.NPM1WPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 156:
					//view = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 157:
					//view = new YellowstonePathology.Business.Test.MPNFish.MPNFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 158:
					//view = new YellowstonePathology.Business.Test.MDSByFish.MDSByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 159:
					//view = new YellowstonePathology.Business.Test.MPL.MPLWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 160:
					//view = new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 161:
					//view = new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 162:
					//view = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCRWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 163:
					//view = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 164:
					//view = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 168:
					//view = new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 169:
					//view = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 170:
					//view = new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 171:
					//view = new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 172:
					//view = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 173:
					//view = new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 174:
					//view = new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile.NeoARRAYSNPCytogeneticProfileWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 175:
					//view = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 177:
					//view = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 178:
					//view = new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 179:
					//view = new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 181:
					//view = new YellowstonePathology.Business.Test.CKIT.CKITWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 183:
					//view = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 184:
					//view = new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 185:
					//view = new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 186:
					//view = new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 192:
					//view = new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
                case 204:
                    //view = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 214:
                    //view = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 215:
                    //view = new YellowstonePathology.Business.Test.PDL1.PDL1WPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 217:
                    //view = new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 218:
                    //view = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 222:
                    //view = new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 223:
                    //view = new YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 225:
                    //view = new YellowstonePathology.Business.Test.BCL2t1418ByPCR.BCL2t1418ByPCRWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 226:
                    //view = new YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 227:
                    //view = new YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR.CCNDIBCLIGHByPCRWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 228:
                    //view = new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 229:
                    //view = new YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 231:
                    //view = new YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
            }
            return view;
        }
    }
}
