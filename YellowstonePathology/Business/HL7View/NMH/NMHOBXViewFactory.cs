using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.NMH
{
    public class NMHOBXViewFactory
    {
        public static NMHOBXView GetObxView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            NMHOBXView view = null;
            switch (panelSetId)
            {
                case 1:
					//view = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;                
                case 3:
                    //view = new YellowstonePathology.Business.Test.NGCT.NGCTWPHOBXView(accessionOrder, reportNo, obxCount);
					break;
				case 46:
					//view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 13:
                case 128:
				    view = new YellowstonePathology.Business.Test.Surgical.SurgicalNMHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 14:
					view = new YellowstonePathology.Business.Test.HPV.HPVNMHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 15:
					view = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapNMHOBXView(accessionOrder, reportNo, obxCount);
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
					view = new YellowstonePathology.Business.Test.HPV1618.HPV1618NMHOBXView(accessionOrder, reportNo, obxCount);
                    break;
               case 66:
					//view = new YellowstonePathology.Business.Test.TestCancelled.TestCancelledWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 79:
                    //view = new YellowstonePathology.Business.Test.PMLRARAByFish.PMLRARAByFishWPHOBXView(accessionOrder, reportNo, obxCount);
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
                case 125:
                    //view = new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWPHOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 131:
                    //view = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelWPHOBXView(accessionOrder, reportNo, obxCount);
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
            }
            return view;
        }
    }
}
