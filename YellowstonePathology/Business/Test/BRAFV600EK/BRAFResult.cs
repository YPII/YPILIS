using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
    public class BRAFResult
    {
        public static string Detected = "BRAF Mutation V600E DETECTED.";
        public static string NotDetected = "BRAF Mutation V600E NOT DETECTED.";

        public static string Method = "Following lysis of paraffin embedded tissue; highly purified DNA was extracted from the specimen using an automated method.  " +
            "PCR amplification using fluorescently labeled primers was then performed.  The products of the PCR reaction were then separated by high resolution capillary " +
            "electrophoresis to look for the presence of the 107 nucleotide fragment indicative of a BRAF V600E mutation.";
        
		public static string ColoRectalCancerReferences = "1.   Benvenuti S, Sartore-Bianchi A, Di Nicolantonio F, et al. Oncogenic activation of the RAS/RAF signaling " +
			"pathway impairs the response of metastatic colorectal cancers to anti-epidermal growth factor receptor antibody therapies. Cancer Res. 2007; 67(6): 2643-2648.\r\n" +
			"2.  Di Nicolantonio F, Martini M, Molinari F, et al. Wild-type BRAF is required for response to panitumumab or cetuximab in metastatic colorectal cancer. J " +
			"Clin Oncol. 2008; 26(35): 5705-5712.\r\n" +
			"3.  Cappuzzo F, Varella-Garcia M, Finocchiaro G, et al. Primary resistance to cetuximab therapy in EGFR FISH-positive colorectal cancer patients. Br J Cancer " +
			"2008; 99: 83-89.\r\n" +
			"4.  Barault L, Veyrie N, Jooste V, et al. Mutations in the RAS-MAPK, PI(3)K (phosphotidylinositol-3-OH kinase) signaling network correlate with poor survival " +
			"in a population-based series of colon cancers. Int J Cancer. 2008; 122(10): 2255-2259.\r\n" +
			"5.  Di Nicolantonio F, Sartori-Bianchi A, Molinari F, et al. BRAF, PIK3CA, and KRAS mutations and loss of PTEN expression impair response to EGFR-targeted " +
			"therapies in metastatic colorectal cancer. In: Proceedings of the 100th Annual Meeting of the American Association for Cancer Research; April 18-22, 2009; " +
			"Denver, CO: AACR; 2009. Abstract LB-93.";

    }
}
