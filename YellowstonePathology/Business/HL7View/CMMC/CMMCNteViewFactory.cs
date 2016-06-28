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
                    //view = new CMMCJak2MutationNteView(accessionOrder, reportNo);
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
                    //view = new CMMCThrombocytopeniaProfileNteView(accessionOrder, reportNo);
                    break;
                case 22:
                    //view = new CMMCPAANteView(accessionOrder, reportNo);
                    break;
                case 23:
                    //view = new CMMCReticulatedPlateletNteView(accessionOrder, reportNo);
                    break;
                case 24:
                    //view = new CMMCStemCellEnumerationNteView(accessionOrder, reportNo);
                    break;
                case 25:
                    //view = new CMMCHpv16NteView(accessionOrder, reportNo);
                    break;
                case 27:
                    //view = new CMMCKrasNteView(accessionOrder, reportNo);
                    break;
                case 30:
                    //view = new CMMCKrasWithBrafReflexNteView(accessionOrder, reportNo);
                    break;
                case 32:
                    //view = new CMMCFactorVNteView(accessionOrder, reportNo);
                    break;
                case 33:
                    //view = new CMMCProthrombinNteView(accessionOrder, reportNo);
                    break;
                case 34:
                    //view = new CMMCMthfrNteView(accessionOrder, reportNo);
                    break;
                case 35:
                    //view = new CMMCAutopsyNteView(accessionOrder, reportNo);
                    break;
                case 36:
                    //view = new CMMCBCellClonalityNteView(accessionOrder, reportNo);
                    break;
                case 46:
                    view = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHCMMCNteView(accessionOrder, reportNo);
                    break;
                case 54:
                    //view = new CMMCCytogeneticsNteView(accessionOrder, reportNo);
                    break;                
                case 60:
                    //view = new CMMCEgfrNteView(accessionOrder, reportNo);
                    break;
                case 61:
					view = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasCMMCNteView(accessionOrder, reportNo);
                    break;
				case 62:                
                    view = new YellowstonePathology.Business.Test.HPV1618.HPV1618CMMCNteView(accessionOrder, reportNo);
					break;
                case 106:
					view = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationCMMCNteView(accessionOrder, reportNo);
					break;
				case 116:
					view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileCMMCNteView(accessionOrder, reportNo);
					break;
                case 186:
                    view = new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHCMMCNteView(accessionOrder, reportNo);
                    break;
                case 213:
                    view = new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRCMMCView(accessionOrder, reportNo);
                    break;
                case 214:
                    view = new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearCMMCNteView(accessionOrder, reportNo);
                    break;
                case 228:
                    view = new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRCMMCNTEView(accessionOrder, reportNo);
                    break;
            }
            return view;
        }        
    }
}
