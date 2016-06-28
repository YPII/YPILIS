using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PanelSetCollection : ObservableCollection<PanelSet>
    {
        public PanelSetCollection()
        {
            
        }        

        public static PanelSetCollection GetAll()
        {
            PanelSetCollection panelSetCollection = new PanelSetCollection();
            //NEO
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired3()); 
			panelSetCollection.Add(new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest()); 
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMultipleFISHProbePanelRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MultipleFISHProbe.MultipleFISHProbeTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetALKRetired());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest()); 
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetPMLRARAByFish()); 
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetPMLRARAByPCR()); 
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetCLLPrognosticPanel());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetCLLByFishPanelRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.CLLByFish.CLLByFishTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMYC()); 
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetFLT3Retired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.FLT3.FLT3Test()); 
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetBCRABLFishRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishTest()); 
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetCEBPARetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.CEBPA.CEBPATest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMPLRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MPL.MPLTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetNPM1Retired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.NPM1.NPM1Test());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByIHCRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ChromosomeAnalysis.ChromosomeAnalysisTest());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetPDGFRa());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetPDGFRb());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetFIP1L1());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetTCellClonalityByPCRRetired());  
			panelSetCollection.Add(new YellowstonePathology.Business.Test.TCellRecepterGammaGeneRearrangement.TCellRecepterGammaGeneRearrangementTest());  
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMDSByFishRetired());  
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MDSByFish.MDSByFishTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MDSExtendedByFish.MDSExtendedByFishTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetIGVH());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetCMVISH());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetCKITRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.CKIT.CKITTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetIgHBCL2ByFish());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetIgHMFABByFish());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.BCL1t1114.BCL1t1114Test());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetAMLPanelRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.AMLStandardByFish.AMLStandardByFishTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMPNFishPanelRetired());         
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MPNFish.MPNFishTest());         
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetJAK2Exon1214Retired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test());  
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetNeoTypeAMLPrognosticPanel());  
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTest());  
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetBCRABLByPCRRetired());  
			panelSetCollection.Add(new YellowstonePathology.Business.Test.BCRABLByPCR.BCRABLByPCRTest());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetConciseBrainTumorProfile());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetEBERByISH());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMYCIgHEN8());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHighGradeLargeBCellLymphomaPanelRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma.HighGradeLargeBCellLymphomaTest());           
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetFlowCytometry());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetBRAFV600EV600KRetired());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMultipleMyelomaMGUSByFishRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish.MultipleMyelomaMGUSByFishTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest());  
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetCalreticulinMutationAnalysisRetired());  
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis.MicrosatelliteInstabilityAnalysisTest());  
            panelSetCollection.Add(new YellowstonePathology.Business.Test.ABL1KinaseDomainMutation.ABL1KinaseDomainMutationTest());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHerpesVirus8ByImmunohistochemistry());  
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHighRiskMultipleMyelomaMGUSByFish());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ZAP70LymphoidPanel.ZAP70LymphoidPanelTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH.CCNDIBCLIGHByFISHTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetIgHMAFByFish());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetP53ByFish());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASExon23Mutation.KRASExon23MutationTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MYD88MutationAnalysis.MYD88MutationAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.BCellGeneRearrangement.BCellGeneRearrangementTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.NRASMutationAnalysis.NRASMutationAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.DeletionsForGlioma1p19q.DeletionsForGlioma1p19qTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BladderCancerFISHUrovysion.BladderCancerFISHUrovysionTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.API2MALT1ByFISH.API2MALT1ByFISHTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.ALK1Test());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CultureAndHoldForCytogeneticsTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.DirectHarvestForFISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CTNNB1Test());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ALLAdultByFISH.ALLAdultByFISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.NeoTYPEMelanomaProfile());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PTENTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MALT1.MALT1Test());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.ETV6RUNX1Test());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.HighRiskHPVByISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.FISH820Q20Test());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.AndrogenRecepter.AndrogenReceptorTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MDSCMMLProfile.MDSCMMLProfileTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.AMLExtendedByFish.AMLExtendedByFishTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CBFBinv16.CBFBinv16Test());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MGMTPromoterMethylationAnalysis.MGMTPromoterMethylationAnalysisTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.IDH1IDH2MutationAnalsysis.IDH1IDH2MutationAnalsysisTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BCL2t1418ByPCR.BCL2t1418ByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BCL2t1418ByFISH.BCL2t1418ByFISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR.CCNDIBCLIGHByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.RUNX1RUNX1T1AML1ETOTranslocation.RUNX1RUNX1T1AML1ETOTranslocationTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.FGFR1.FGFR1Test());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CSF3RMutationAnalysis.CSF3RMutationAnalysisTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KappaLambdaByISH.KappaLambdaByISHTest());

            //ARUP
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV1618.HPV1618Test());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupBraf());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetDNAContent());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupCysticFibrosis());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupHer2Fish());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupHpv());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupHPV1618Retired());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupJak2());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupKras());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupKrasRflx());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupNgct());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMLH1Retired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupLiverIronQuantitation());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupAlpha1AntitrypsinStain());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupFactorV());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetArupProthrombin());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.NonHodgkinsLymphomaFISHPanel.NonHodgkinsLymphomaFISHPanelTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.EosinophiliaByFISH.EosinophiliaByFISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfileTest.NeoARRAYSNPCytogeneticProfileTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetNextGenMyeloidDisordersProfile());

            //YPII
            panelSetCollection.Add(new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Autopsy.AutopsyTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.CysticFibrosisTestRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetERPRForDCIS());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetFNASampleCollection());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired1());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired2());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetHPV16());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV.HPVTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.KRASStandardTestRetired());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMthfr());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.NGCT.NGCTTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.Surgical.SurgicalTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.ReportSummary());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.TestCancelled.TestCancelledTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetNonGYNCytology());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.Prothrombin.ProthrombinTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.PeerReview.PeerReviewTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.IHCQC.IHCQCTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.ExtractAndHoldForMolecular.ExtractAndHoldForMolecularTest());

            panelSetCollection.Add(new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest());

            panelSetCollection.Add(new YellowstonePathology.Business.Test.PNH.PNHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinTest());
			panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.HighRiskHPVTestRetired());

            panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.InformalConsult.InformalConsultTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisTest());

            //UniversityofWashington
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetUniversalOrganismByPCR());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetRenalBiopsyPanel());

            //Billings Clinic
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetDirectImmunoFluorescence());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetImmunohistochemistry());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetGlycophorinA());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.PDL1.PDL1Test());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CD1a.CD1aTest());

            //TheraPath
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMusclePathologyAnalysis());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetNervePathologyAnalysis());

            //Genomic Health
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetOncoTypeDX());

            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetAnatomicPathologyConsultation());

            //ReflexTesting            
            panelSetCollection.Add(new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest());
            panelSetCollection.Add(new PanelSetJAK2PolycythemiaVeraReflex());
            panelSetCollection.Add(new PanelSetJAK2PrimaryMyelofibrosisReflex());
            panelSetCollection.Add(new PanelSetJAK2EssentialThrombocythemiaReflex());			            			

            panelSetCollection.Add(new PanelSetFishAnalysisForSYTGeneRearrangement());
            panelSetCollection.Add(new PanelSetHPVLowRiskByInSituHybridization());

            panelSetCollection.Add(new PanelSetFoundationOneGeneticTesting());

            panelSetCollection.Add(new PanelSetIHCHerpesVirus());
            panelSetCollection.Add(new PanelSetCytogeneticAnalysisRetired());

			panelSetCollection.Add(new YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly.ChromosomeAnalysisForFetalAnomalyTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MissingInformation.MissingInformationTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Trisomy21ByFISH.Trisomy21ByFISHTest());

            PanelSetCollection result = new PanelSetCollection();
            IEnumerable<PanelSet> enumerable = panelSetCollection.OrderBy(i => i.PanelSetName);
            foreach (PanelSet item in enumerable)
            {
                result.Add(item);
            }

            return result;
        }

        public static PanelSetCollection GetAllActive()
        {
            PanelSetCollection result = new PanelSetCollection();
            PanelSetCollection allPanelSets = GetAll();
            foreach (PanelSet panelSet in allPanelSets)
            {
                if (panelSet.Active == true)
                {
                    result.Add(panelSet);
                }                
            }
            return result;
        }

        public PanelSet GetByUniversalServiceIdTestCode(string testCode)
        {
            PanelSet result = null;
            foreach (PanelSet panelSet in this)
            {
                if (panelSet.UniversalServiceIdCollection.Exists(testCode) == true)
                {
                    result = panelSet;
                    break;
                }
            }
            return result;
        }

        public static PanelSetCollection GetAllMolecularTest()
        {
            PanelSetCollection result = new PanelSetCollection();
            PanelSetCollection allPanelSets = PanelSetCollection.GetAll();

            foreach (PanelSet panelSet in allPanelSets)
            {
                if (panelSet is PanelSetMolecularTest == true)
                {
                    result.Add(panelSet);
                }
            }

            return result;
        }

        public static PanelSetCollection GetFlowPanelSets(bool includePNH)
        {
            PanelSetCollection result = new PanelSetCollection();
            PanelSetCollection allPanelSets = PanelSetCollection.GetAll();

            foreach (PanelSet panelSet in allPanelSets)
            {
                if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FlowCytometry)
                {
                    if (includePNH == true)
                    {
                        result.Add(panelSet);
                    }
                    else
                    {
                        if (panelSet.PanelSetId != 19)
                        {
                            result.Add(panelSet);
                        }
                    }
                }
            }

            return result;
        }

        public static PanelSetCollection GetByFacility(YellowstonePathology.Business.Facility.Model.Facility facility)
        {
            PanelSetCollection allPanelSets = PanelSetCollection.GetAll();
            PanelSetCollection result = new PanelSetCollection();

            foreach (PanelSet panelSet in allPanelSets)
            {
                if (panelSet.TechnicalComponentFacility != null && panelSet.TechnicalComponentFacility.FacilityId == facility.FacilityId && panelSet.Active == true)
                {
                    result.Add(panelSet);
                }
            }

            return result;
        }

        public static PanelSetCollection GetByCaseType(string caseType)
        {
            PanelSetCollection result = new PanelSetCollection();
            PanelSetCollection allPanelSets = PanelSetCollection.GetAll();

            IEnumerable<PanelSet> enumberable = allPanelSets.Where(w => w.CaseType == caseType && w.Active == true);
            foreach (PanelSet item in enumberable)
            {
                result.Add(item);
            }

            return result;
        }        

        public static List<string> GetCaseTypes()
        {
            PanelSetCollection allPanelSets = PanelSetCollection.GetAll();
            List<string> result = (from x in allPanelSets orderby x.CaseType select x.CaseType).Distinct<string>().ToList<string>();
            result.Add(YellowstonePathology.Business.CaseType.ALLCaseTypes);
            return result;
        }

        public static PanelSetCollection GetHistologyPanelSets()
        {
            PanelSetCollection panelSetCollection = new PanelSetCollection();
			panelSetCollection.Add(new YellowstonePathology.Business.Test.Surgical.SurgicalTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest());            
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest());
            panelSetCollection.Add(new PanelSetERPRForDCIS());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTest());
            panelSetCollection.Add(new PanelSetFNASampleCollection());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.Autopsy.AutopsyTest());
            panelSetCollection.Add(new PanelSetRenalBiopsyPanel());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.IHCQC.IHCQCTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.MissingInformation.MissingInformationTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.InformalConsult.InformalConsultTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HoldForFlow.HoldForFlowTest());
            return panelSetCollection;
        }

        public static PanelSetCollection GetPathologistPanelSets()
        {
            PanelSetCollection panelSetCollection = new PanelSetCollection();
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Surgical.SurgicalTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCRTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest());
            panelSetCollection.Add(new PanelSetERPRForDCIS());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest());
            panelSetCollection.Add(new PanelSetJAK2PolycythemiaVeraReflex());
            panelSetCollection.Add(new PanelSetJAK2PrimaryMyelofibrosisReflex());
            panelSetCollection.Add(new PanelSetJAK2EssentialThrombocythemiaReflex());                            
            //panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest());
            //panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest());
            return panelSetCollection;
        }

		public PanelSet GetPanelSet(int panelSetId)
		{
			PanelSet result = null;
			foreach (PanelSet panelSet in this)
			{
				if (panelSetId == panelSet.PanelSetId)
				{
					result = panelSet;
					break;
				}
			}
			return result;
		}

        public static PanelSetCollection GetYPIOrderTypes()
        {
            PanelSetCollection result = new PanelSetCollection();
			result.Add(new YellowstonePathology.Business.Test.Surgical.SurgicalTest());
			result.Add(new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest());
            return result;
        }

		public static PanelSetCollection GetReflexTestingPanelSets()
		{
			PanelSetCollection panelSetCollection = new PanelSetCollection();
			panelSetCollection.Add(new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest());
            panelSetCollection.Add(new PanelSetJAK2PolycythemiaVeraReflex());
            panelSetCollection.Add(new PanelSetJAK2PrimaryMyelofibrosisReflex());
            panelSetCollection.Add(new PanelSetJAK2EssentialThrombocythemiaReflex());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest());
			panelSetCollection.Add(new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest());
			return panelSetCollection;
		}

        public bool IsMolecular(int panelSetId)
        {
            bool result = false;
            foreach (PanelSet panelSet in this)
            {
                if (panelSet.PanelSetId == panelSetId)
                {
                    if (panelSet is PanelSetMolecularTest == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool IsMolecularWithSplitCodes(int panelSetId)
        {
            bool result = false;
            foreach (PanelSet panelSet in this)
            {
                if (panelSet.PanelSetId == panelSetId)
                {
                    if (panelSet is PanelSetMolecularTest == true)
                    {
                        PanelSetMolecularTest panelSetMolecularTest = (PanelSetMolecularTest)panelSet;
                        if (panelSetMolecularTest.HasSplitCPTCode == true)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

		public List<string> GetPathologistsCaseTypes()
		{
            List<string> result = PanelSetCollection.GetCaseTypes();
			result.Insert(0, "All Case Types");
			return result;
		}

        public static List<int> GetPanelSetIdList(string caseType)
        {
            PanelSetCollection allPanelSets = GetAll();
            List<int> result = new List<int>();
            if (caseType != YellowstonePathology.Business.CaseType.ALLCaseTypes)
            {
                result = (from x in allPanelSets where x.CaseType == caseType select x.PanelSetId).ToList<int>();
            }
            else
            {
                result = (from x in allPanelSets select x.PanelSetId).ToList<int>();
            }
            return result;
        }

        public static string GetPanelSetIdString(string caseType)
        {            
            List<int> intList = GetPanelSetIdList(caseType);
			return YellowstonePathology.Business.Helper.IdHelper.ToIdString(intList);         
        }

        public static PanelSetCollection GetMolecularLabPanelSets()
        {
            PanelSetCollection panelSetCollection = new PanelSetCollection();
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Autopsy.AutopsyTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BCellClonalityByPCR.BCellClonalityByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.FactorVLeiden.FactorVLeidenTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV.HPVTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV1618.HPV1618Test());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest());
            panelSetCollection.Add(new YellowstonePathology.Business.PanelSet.Model.PanelSetMthfr());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.NGCT.NGCTTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Prothrombin.ProthrombinTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest());
            panelSetCollection.Add(new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest());
            return Sort(panelSetCollection);
        }

        private static PanelSetCollection Sort(PanelSetCollection panelSetCollection)
        {
            PanelSetCollection result = new PanelSetCollection();
            IOrderedEnumerable<PanelSet> orderedResult = panelSetCollection.OrderBy(i => i.PanelSetName);
            foreach (PanelSet panelSet in orderedResult)
            {
                result.Add(panelSet);
            }
            return result;
        }
    }
}
