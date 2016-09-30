using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTResult : YellowstonePathology.Business.Test.TestResult
	{
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string NGPositiveResultCode = "NGPSTV";
        public static string NGNegativeResultCode = "NGNGTV";
        public static string CTPositiveResultCode = "CTPSTV";
        public static string CTNegativeResultCode = "CTNGTV";
        public static string InvalidResultCode = "NVLD";
        public static string InvalidResult = "Invalid";

        public static string Method = "The APTIMA COMBO 2 Assay is a target amplification nucleic acid probe test that utilizes target capture for the " +
            "in vitro qualitative detection and differentiation of ribosomal RNA (rRNA) from Chlamydia trachomatis (CT) and/or Neisseria gonorrhoeae (GC) to aid in " +
            "the diagnosis of chlamydial and/or gonococcal urogenital disease using the PANTHER System.";
        public static string References = "Gaydos, C. A., T.C. Quinn, D. Willis, A. Weissfeld, E. W. Hook, D. H. Martin, D. V. Ferraro, and J. Schachter. 2003. " +
            "Performance of the APTIMA COMBO 2 Assay for detection of Chlamydia trachomatis and Neisseria gonorrhoeae in female urine and endocervical swab " +
            "specimens. J. Clin. Microbiol. 41:304-309.";
        public static string TestInformation = "The APTIMA COMBO 2 Assay qualitatively detects CT and/or GC rRNA in clinician-collected endocervical, vaginal, and male " +
            "urethral swab specimens, patient-collected vaginal swab specimens, PreservCyt Solution liquid Pap specimens, and in female and male urine specimens " +
            "from symptomatic and asymptomatic individuals. Per the manufacturer, Chlamydia trachomatis analytical sensitivity (limits of detection) was determined " +
            "by directly comparing dilutions of CT organisms in cell culture and in the assay. The analytical sensitivity claim for the assay is one Inclusion-Forming " +
            "Unit (IFU) per assay (7.25 IFU/swab, 5.0 IFU/ mL urine, 9.75 IFU/mL PreservCyt Solution liquid Pap) for all 15 CT serovars (A, B, Ba, C, D, E, F, G, H, " +
            "I, J, K, L1, L2 and L3). However, dilutions of less than 1.0 IFU/assay of all serovars tested positive in the APTIMA COMBO 2 Assay. Neisseria gonorrhoeae " +
            "analytical sensitivity was determined by directly comparing dilutions of 57 different clinical isolates in culture and in the APTIMA COMBO 2 Assay with " +
            "swab and urine specimens and 20 clinical isolates with PreservCyt Solution liquid Pap specimens. The analytical sensitivity claim for the assay is 50 " +
            "cells/assay (362 cells/swab, 250 cells/mL urine, 488 cells/mL PreservCyt Solution liquid Pap).";        

        protected string m_Method;
		protected string m_References;		
        protected string m_TestInformation;

        protected string m_NeisseriaGonorrhoeaeResult;
        protected string m_ChlamydiaTrachomatisResult;
        protected string m_NGResultCode;
        protected string m_CTResultCode;

        public NGCTResult()
		{
            this.m_Method = NGCTResult.Method;
            this.m_References = NGCTResult.References; ;			
            this.m_TestInformation = NGCTResult.TestInformation;
		}
	}
}
