using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelResult
    {        
        public static string BRAFAbbreviation = "BRAF";
        public static string KRASAbbreviation = "KRAS";
        public static string HRASAbbreviation = "HRAS";
        public static string NRASAbbreviation = "NRAS";

        public static string Method = "Nucleic acid was isolated from cells or microdissection-enriched FFPE tissue. The RAS/RAF Profile includes massive " +
    		"parallel sequencing and mutation analysis of the following genes: BRAF: EX: 11 and 15 (AA: G2695_K2727 and D2725_K2756); HRAS: EX: 2 and 3 (AA: " +
    		"M1_G15 and D38_E62); KRAS: EX: 2, 3 and 4 (AA: M1_I21, D38_E62, and K104_A146); and NRAS: EX: 2, 3 and 4 (AA: M1_A18, D38_E62, and N104_A146). " +
    		"When applicable, confirmation of the presence or lack of mutation was performed using Sanger sequencing on the following genes: BRAF, HRAS, KRAS, " +
    		"and NRAS. This sequencing method has a typical sensitivity of 5% for detecting mutations. Various factors including quantity and quality of nucleic " +
    		"acid, sample preparation and sample age can affect assay performance.";

        public static string References = "1.  Peeters M, Douillard J, Van Cutsem E, Siena S, Zhang K, Williams R, Wiezorek J Mutant KRAS codon 12 and 13 " +
        	"alleles in patients with metastatic colorectal cancer: assessment as prognostic and predictive biomarkers of response to panitumumab." + Environment.NewLine +
        	"2.  Bokemeyer C, Van Cutsem E, Rougier P, Ciardiello F, Heeger S, Schlichting M, Celik I, Köhne C Addition of cetuximab to chemotherapy as first-line" +
        	"treatment for KRAS wild-type metastatic colorectal cancer: pooled analysis of the CRYSTAL and OPUS randomised clinical trials." + Environment.NewLine +
        	"3.  Di Nicolantonio F, Martini M, Molinari F, Sartore-Bianchi A, Arena S, Saletti P, De Dosso S, Mazzucchelli L, Frattini M, Siena S, Bardelli A " +
        	"Wild-type BRAF is required for response to panitumumab or cetuximab in metastatic colorectal cancer." + Environment.NewLine +
        	"4.  De Roock W, Claes B, Bernasconi D, De Schutter J, Biesmans B, Fountzilas G, Kalogeras K, Kotoula V, Papamichael D, Laurent-Puig P, Penault-Llorca " +
        	"F, Rougier P, Vincenzi B, Santini D, Tonini G, CappuzzoF, Frattini M, Molinari F, Saletti P, De Dosso S, Martini M, Bardelli A, Siena S, " +
        	"Sartore-Bianchi A, Tabernero J, Macarulla T, Di Fiore F, Gangloff A, Ciardiello F, Pfeiffer P, Qvortrup C, Hansen T, Van Cutsem E, Piessevaux H, " +
        	"Lambrechts D, Delorenzi M, Tejpar S Effects of KRAS, BRAF, NRAS, and PIK3CA mutations on the efficacy of cetuximab plus chemotherapy in " +
        	"chemotherapy-refractory metastatic colorectal cancer: a retrospective consortium analysis.";

        public static string DetectedResult = "Detected";
        public static string NotDetectedResult = "Not Detected";
        public static string NAResult = "N/A";               
                        
        protected const string m_TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has " +
            "not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high " +
            "complexity clinical testing.";

        protected string m_Comment;

        protected string m_InterpretationFirstLine;        
        protected string m_InterpretationBody = "KRAS, NRAS, HRAS, and BRAF are members of the RAS/RAF/MAPK pathway. Current guidelines require KRAS and NRAS " +
            "testing in metastatic colorectal cancer for determination of anti-EGFR therapy, and recommend BRAF testing as a marker of poor prognosis.  Patients " +
            "with any known KRAS mutation (exon 2 or non-exon 2) or NRAS should not be treated with either cetuximab or panitumumab per NCCN Guidelines. BRAF " +
            "mutations may predict lack of response to anti-EGFR therapy; evidence is mixed.";

        public RASRAFPanelResult()
        {

        }

        public virtual void SetResults(RASRAFPanelTestOrder testOrder, ResultCollection resultCollection)
        {
            StringBuilder interpretation = new StringBuilder();
            interpretation.AppendLine(this.m_InterpretationFirstLine);            
            interpretation.AppendLine();
            interpretation.Append(this.m_InterpretationBody);

            interpretation = interpretation.Replace("[NOTDETECTEDMUTATIONLIST]", resultCollection.GetNotDetectedListString());
            interpretation = interpretation.Replace("[DETECTEDMUTATIONLIST]", resultCollection.GetDetectedListString());

            testOrder.Interpretation = interpretation.ToString();            

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
            disclaimer.Append(m_TestDevelopment);
            testOrder.ReportDisclaimer = disclaimer.ToString();
        }
        
        public static void SetBRAFDetected(RASRAFPanelTestOrder testOrder)
        {
        	if(testOrder.BRAFAlternateNucleotideMutationName == RASRAFPanelResult.NAResult) testOrder.BRAFAlternateNucleotideMutationName = null;
        	if(testOrder.BRAFConsequence == RASRAFPanelResult.NAResult) testOrder.BRAFConsequence = null;
        	if(testOrder.BRAFMutationName == RASRAFPanelResult.NAResult) testOrder.BRAFMutationName = null;
        	if(testOrder.BRAFPredictedEffectOnProtein == RASRAFPanelResult.NAResult) testOrder.BRAFPredictedEffectOnProtein = null;
        }
        
        public static void SetBRAFNotDetected(RASRAFPanelTestOrder testOrder)
        {
			testOrder.BRAFAlternateNucleotideMutationName = RASRAFPanelResult.NAResult;
			testOrder.BRAFConsequence = RASRAFPanelResult.NAResult;
			testOrder.BRAFMutationName = RASRAFPanelResult.NAResult;
			testOrder.BRAFPredictedEffectOnProtein = RASRAFPanelResult.NAResult;
        }
        
        public static void SetKRASDetected(RASRAFPanelTestOrder testOrder)
        {
        	if(testOrder.KRASAlternateNucleotideMutationName == RASRAFPanelResult.NAResult) testOrder.KRASAlternateNucleotideMutationName = null;
        	if(testOrder.KRASConsequence == RASRAFPanelResult.NAResult) testOrder.KRASConsequence = null;
        	if(testOrder.KRASMutationName == RASRAFPanelResult.NAResult) testOrder.KRASMutationName = null;
        	if(testOrder.KRASPredictedEffectOnProtein == RASRAFPanelResult.NAResult) testOrder.KRASPredictedEffectOnProtein = null;
        }
        
        public static void SetKRASNotDetected(RASRAFPanelTestOrder testOrder)
        {
			testOrder.KRASAlternateNucleotideMutationName = RASRAFPanelResult.NAResult;
			testOrder.KRASConsequence = RASRAFPanelResult.NAResult;
			testOrder.KRASMutationName = RASRAFPanelResult.NAResult;
			testOrder.KRASPredictedEffectOnProtein = RASRAFPanelResult.NAResult;
        }
        
        public static void SetNRASDetected(RASRAFPanelTestOrder testOrder)
        {
        	if(testOrder.NRASAlternateNucleotideMutationName == RASRAFPanelResult.NAResult) testOrder.NRASAlternateNucleotideMutationName = null;
        	if(testOrder.NRASConsequence == RASRAFPanelResult.NAResult) testOrder.NRASConsequence = null;
        	if(testOrder.NRASMutationName == RASRAFPanelResult.NAResult) testOrder.NRASMutationName = null;
        	if(testOrder.NRASPredictedEffectOnProtein == RASRAFPanelResult.NAResult) testOrder.NRASPredictedEffectOnProtein = null;
        }
        
        public static void SetNRASNotDetected(RASRAFPanelTestOrder testOrder)
        {
			testOrder.NRASAlternateNucleotideMutationName = RASRAFPanelResult.NAResult;
			testOrder.NRASConsequence = RASRAFPanelResult.NAResult;
			testOrder.NRASMutationName = RASRAFPanelResult.NAResult;
            testOrder.NRASPredictedEffectOnProtein = RASRAFPanelResult.NAResult;
        }
        
        public static void SetHRASDetected(RASRAFPanelTestOrder testOrder)
        {
        	if(testOrder.HRASAlternateNucleotideMutationName == RASRAFPanelResult.NAResult) testOrder.HRASAlternateNucleotideMutationName = null;
        	if(testOrder.HRASConsequence == RASRAFPanelResult.NAResult) testOrder.HRASConsequence = null;
        	if(testOrder.HRASMutationName == RASRAFPanelResult.NAResult) testOrder.HRASMutationName = null;
        	if(testOrder.HRASPredictedEffectOnProtein == RASRAFPanelResult.NAResult) testOrder.HRASPredictedEffectOnProtein = null;
        }
        
        public static void SetHRASNotDetected(RASRAFPanelTestOrder testOrder)
        {
			testOrder.HRASAlternateNucleotideMutationName = RASRAFPanelResult.NAResult;
			testOrder.HRASConsequence = RASRAFPanelResult.NAResult;
			testOrder.HRASMutationName = RASRAFPanelResult.NAResult;
            testOrder.HRASPredictedEffectOnProtein = RASRAFPanelResult.NAResult;
        }
    }
}
