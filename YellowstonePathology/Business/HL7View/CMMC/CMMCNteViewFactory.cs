using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.CMMC
{
    public class CMMCNteViewFactory
    {
        public static CMMCNteView GetNteView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            CMMCNteView view = null;
            switch (panelSetId)
            {
                case 1:
                    view = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FCMMCNteView(accessionOrder, reportNo);
                    break;
                case 2:
                    //view = new CMMCCFNteView(accessionOrder, reportNo);
                    break;
                case 3:
                    view = new YellowstonePathology.Business.Test.NGCT.NGCTCMMCNteView(accessionOrder, reportNo);
					break;
                case 13:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalCMMCNteView(accessionOrder, reportNo);
                    break;
                case 14:
                    view = new YellowstonePathology.Business.Test.HPV.HPVCMMCNteView(accessionOrder, reportNo);
                    break;
                case 15:
                    view = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapCMMCNteView(accessionOrder, reportNo);
                    break;
                case 18:
					view = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKCMMCNteView(accessionOrder, reportNo);
                    break;
                case 19:
                    view = new YellowstonePathology.Business.Test.PNH.PNHCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 20:
                    view = new YellowstonePathology.Business.Test.LLP.LLPCMMCNteView(accessionOrder, reportNo);
                    break;
                case 21:
                    view = new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 46:
                    view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHCMMCNteView(accessionOrder, reportNo);
                    break;
                case 61:
					view = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasCMMCNteView(accessionOrder, reportNo);
                    break;
				case 62:                
                    view = new YellowstonePathology.Business.Test.HPV1618.HPV1618CMMCNteView(accessionOrder, reportNo);
					break;
                case 102:
                    view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 106:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationCMMCNteView(accessionOrder, reportNo);
					break;
				case 116:
					view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileCMMCNteView(accessionOrder, reportNo);
					break;
                case 141:
                    view = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214CMMCNteView(accessionOrder, reportNo);
                    break;
                case 148:
                    view = new YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 150:
                    view = new YellowstonePathology.Business.Test.CEBPA.CEBPACMMCNTEView(accessionOrder, reportNo);
                    break;
                case 151:
                    view = new YellowstonePathology.Business.Test.CLLByFish.CLLByFishCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 153:
                    view = new YellowstonePathology.Business.Test.FLT3.FLT3CMMCNTEView(accessionOrder, reportNo);
                    break;
                case 156:
                    view = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 162:
                    view = new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCRCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 164:
                    view = new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 169:
                    view = new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalCMMCNteView(accessionOrder, reportNo);
                    break;
                case 172:
                    view = new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 177:
                    view = new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 186:
                    view = new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHCMMCNteView(accessionOrder, reportNo);
                    break;
                case 211:
                    view = new YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowCMMCView(accessionOrder, reportNo);
                    break;
                case 213:
                    view = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRCMMCView(accessionOrder, reportNo);
                    break;
                case 214:
                    view = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearCMMCNteView(accessionOrder, reportNo);
                    break;
                case 218:
                    view = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 222:
                    view = new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 226:
                    view = new YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 228:
                    view = new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 245:
                    view = new YellowstonePathology.Business.Test.PDL122C3.PDL22C3CMMCNTEView(accessionOrder, reportNo);
                    break;
                case 269:
                    view = new YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorCMMCView(accessionOrder, reportNo);
                    break;
                case 274:
                    view = new YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 313:
                    view = new YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 338:
                    view = new YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2.ThrombocytopeniaProfileV2CMMCNTEView(accessionOrder, reportNo);
                    break;
                case 365:
                    view = new YellowstonePathology.Business.Test.PDL122C3forEsophagealSquamousCellCarcinoma.PDL122C3EsophSquamCellCarcinomaCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 366:
                    view = new YellowstonePathology.Business.Test.PDL122C3forGastricGEA.PDL122C3forGastricGEACMMCNTEView(accessionOrder, reportNo);
                    break;
                case 367:
                    view = new YellowstonePathology.Business.Test.PDL122C3forHeadandNeck.PDL122C3forHeadandNeckCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 368:
                    view = new YellowstonePathology.Business.Test.PDL122C3forUrothelialCarcinoma.PDL122C3forUrothelialCarcinomaCMMCNTEView(accessionOrder, reportNo);
                    break;
                case 369:
                    view = new YellowstonePathology.Business.Test.PDL122C3forNonsmallCellLungCancer.PDL122C3forNonsmallCellLungCancerCMMCNTEView(accessionOrder, reportNo);
                    break;
            }
            return view;
        }        
    }
}
