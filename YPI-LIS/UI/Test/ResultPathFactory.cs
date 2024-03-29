﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class ResultPathFactory
    {
        public delegate void FinishedEventHandler(object sender, EventArgs e);
        public event FinishedEventHandler Finished;     

        public static ResultPath GetResultPath(int panelSetId,
            string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window,
            System.Windows.Visibility backButtonVisibility)
        {
            ResultPath result = null;
            switch(panelSetId)
            {				
				case 3:
					result = new NGCTResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 14:
                    result = new HPVResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 18:
					result = new BRAFV600EKResultPath(reportNo, accessionOrder, pageNavigator, backButtonVisibility, window);
                    break;
				case 19:
					result = new PNHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 27:
					result = new KRASStandardResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 28:
                    result = new FetalHemoglobinResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 30:
                    result = new KRASStandardReflexResultPath(reportNo, accessionOrder, pageNavigator, window, backButtonVisibility);
                    break;
                case 31:
                    result = new TechnicalOnlyResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 32:
					result = new FactorVLeidenResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 33:
					result = new ProthrombinResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 36:
					result = new BCellClonalityByPCRResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 46:
                    result = new HER2AmplificationByISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 50:
					result = new ErPrSemiQuantitativeResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;				
                case 61:
					result = new TrichomonasResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 62:
					result = new HPV1618ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;                
                case 213:
                    result = new HPV1618ByPCRResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 269:
                    result = new HPV1618SolidTumorResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 66:
					result = new TestCancelledResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;				
				case 81:
                case 82:
                    result = new FNAResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 100:
					result = new BCL1t1114ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 102:
					result = new LynchSyndromeIHCPanelResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 106:
					result = new LynchSyndromeEvaluationResultPath(reportNo, accessionOrder, pageNavigator, window, backButtonVisibility);
                    break;
				case 112:
					result = new ComprehensiveColonCancerProfilePath(reportNo, accessionOrder, pageNavigator, window, backButtonVisibility);
                    break;
                case 124:
                    result = new Test.EGFRToALKReflexPath(reportNo, accessionOrder, pageNavigator, window, System.Windows.Visibility.Collapsed);
                    break;
                case 125:
                    result = new Test.InvasiveBreastPanelPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 131:
                    result = new ALKForNSCLCByFISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 135:
					result = new ABL1KinaseDomainMutationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 136:
					result = new MPNStandardReflexPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 137:
					result = new MPNExtendedReflexPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 140:
					result = new CalreticulinMutationAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 141:
					result = new JAK2Exon1214ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 143:
					result = new ZAP70LymphoidPanelResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 144:
					result = new MLH1MethalationAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 145:
					result = new ChromosomeAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 147:
					result = new MultipleMyelomaMGUSByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 148:
					result = new CCNDIBCLIGHByFISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 149:
					result = new HighGradeLargeBCellLymphomaResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 150:
					result = new CEBPAResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 151:
					result = new CLLByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 152:
					result = new TCellRecepterGammaGeneRearrangementResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;				
				case 155:
					result = new NPM1ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 156:
					result = new BCRABLByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 157:
					result = new MPNFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 158:
					result = new MDSByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 159:
					result = new MPLResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 160:
					result = new MultipleFISHProbePanelResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 161:
					result = new MultipleMyelomaIgHByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 162:
					result = new BCRABLByPCRResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 163:
					result = new Her2AmplificationByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 164:
					result = new MDSExtendedPanelByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 168:
                    result = new AMLStandardByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 169:
					result = new ChromosomeAnalysisForFetalAnomalyResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 170:
					result = new NonHodgkinsLymphomaFISHPanelResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 171:
					result = new Her2AmplificationByIHCResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 172:
					result = new EosinophiliaByFISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 153:
                    result = new FLT3ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 173:
					result = new PlasmaCellMyelomaRiskStratificationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 174:
					result = new NeoARRAYSNPCytogeneticProfileResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 175:
					result = new KRASExon4MutationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 177:
					result = new BCellGeneRearrangementResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 178:
					result = new MYD88MutationAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 179:
					result = new NRASMutationAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 180:
                    result = new IgHMFABByFishResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 181:
					result = new CKITResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 183:
					result = new CysticFibrosisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 184:
					result = new DeletionsForGlioma1p19qResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 185:
					result = new BladderCancerFISHUrovysionResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 186:
					result = new API2MALT1ByFISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 192:
					result = new ALLAdultByFISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 201:
                    result = new IHCQCResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
				case 203:
					result = new ReviewForAdditionalTestingResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 204:
                    result = new ROS1ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 205:
                    result = new AndrogenReceptorResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 211:
                case 359:
                case 360:
                case 361:
                    result = new HoldForFlowResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 212:
                    result = new MissingInformationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 214:
                    result = new TechInitiatedPeripheralSmearResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 215:
                    result = new PDL1SP142ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 245:
                    result = new PDL1SP22C3ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 216:
                    result = new InformalConsultResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 217:
                    result = new KRASExon23MutationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 218:
                    result = new RASRAFPanelResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 222:
                    result = new BCellEnumerationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 223:
                    result = new TCellSubsetAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 225:
                    result = new BCL2t1418ByPCRResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 226:
                    result = new BCL2t1418ByFISHResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 227:
                    result = new CCNDIBCLIGHByPCRResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 228:
                    result = new API2MALT1ByPCRResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 229:
                    result = new AMLNonFavorableRiskResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 230:
                    result = new ExtractAndHoldForMolecularResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 231:
                    result = new RUNX1RUNX1T1AML1ETOTranslocationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 232:
                    result = new FGFR1ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 233:
                    result = new CSF3RMutationAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 234:
                    result = new TCellRecepterBetaGeneRearrangementResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 235:
                    result = new TechnicalOnlyResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 238:
                    result = new GrossOnlyResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 247:
                    result = new TCellNKProfileResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 250:
                    result = new FISH5p159q2215p22ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;                
                case 262:
                    result = new RetrospectiveResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 263:
                    result = new BCellSubsetAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 268:
                    result = new BoneMarrowSummaryResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 274:
                    result = new BRAFMutationAnalysisResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 290:
                    result = new SlideTrackingResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;                
                case 300:
                    result = new ExtractAndHoldForPreauthorizationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;                
                case 313:
                    result = new HER2AmplificationSummaryResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 314:
                    result = new HER2AmplificationRecountResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 328:
                    result = new StemCellEnumerationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 333:
                    result = new FetalHemoglobinV2ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 334:
                    result = new StemCellCD34EnumerationResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 338:
                    result = new ThrombocytopeniaProfileV2ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 339:
                    result = new ReticulatedPlateletAnalysisV2ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 340:
                    result = new PlateletAssociatedAntibodiesV2ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 363:
                    result = new AuthorizationForVerbalTestRequestResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 365:
                    result = new PDL122C3forEsophagealSquamousCellCarcinomaResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 366:
                    result = new PDL122C3forGastricGEAResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 367:
                    result = new PDL122C3forHeadandNeckResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 368:
                    result = new PDL122C3forUrothelialCarcinomaResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 369:
                    result = new PDL122C3forNonsmallCellLungCancerResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 400:
                    result = new SARSCoV2ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 414:
                    result = new PDL122C3forTNBCBreastResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 415:
                    result = new APTIMASARSCoV2ResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;
                case 433:
                    result = new AnalCytologyResultPath(reportNo, accessionOrder, pageNavigator, window);
                    break;            }

            if (result == null)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetId);
                if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
                {
                    result = new PublishedDocumentResultPath(reportNo, accessionOrder, pageNavigator, window);
                }
            }
            return result;
        }


        public bool Start(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window,
            System.Windows.Visibility backButtonVisibility)
        {
			bool result = false;

            if(panelSetOrder.ResultDocumentSource == "PublishedDocument")
            {                
                PublishedDocumentResultPath publishedDocumentResultPath = new PublishedDocumentResultPath(panelSetOrder.ReportNo, accessionOrder, pageNavigator, window);
                publishedDocumentResultPath.Finish += ResultPath_Finish;
                publishedDocumentResultPath.Start();
                return true;
            }

            YellowstonePathology.UI.Test.ResultPath resultPath = UI.Test.ResultPathFactory.GetResultPath(panelSetOrder.PanelSetId, panelSetOrder.ReportNo, accessionOrder, pageNavigator, window, System.Windows.Visibility.Collapsed);

            if (resultPath != null)
            {
				result = true;
                resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Start();
            }
            else
            {
                if (panelSetOrder is YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)
                {
                    result = true;

                    YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(accessionOrder.MasterAccessionNo);

                    if (clientOrders.Count > 0)
                    {
                        clientOrder = clientOrders[0];
                    }

                    YellowstonePathology.UI.Login.WomensHealthProfilePath womensHealthProfilePath = new YellowstonePathology.UI.Login.WomensHealthProfilePath(accessionOrder, clientOrder, pageNavigator, window, System.Windows.Visibility.Collapsed);
                    womensHealthProfilePath.Back += ResultPath_Finish;
                    womensHealthProfilePath.Finish += ResultPath_Finish;
                    womensHealthProfilePath.Start();
                }
                else
                {
                    result = true;
                    UnreachableResultPath unreachableResultPath = new UnreachableResultPath(panelSetOrder.ReportNo, accessionOrder, pageNavigator, window);
                    unreachableResultPath.Finish += ResultPath_Finish;
                    unreachableResultPath.Start();
                }
            }

            return result;
        }        
       
        private void ResultPath_Finish(object sender, EventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new EventArgs());
        }        
    }
}
