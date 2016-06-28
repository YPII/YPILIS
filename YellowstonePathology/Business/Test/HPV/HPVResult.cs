using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV
{
	public class HPVResult : Test.TestResult
	{
        public static string OveralResultCodePositive = "HPVPSTV";

        public static string InvalidResult = "Invalid";
        public static string PositiveResult = "Positive";
		public static string NegativeResult = "Negative";
		public static string IndeterminateResult = "Indeterminate";
		public static string QnsResult = "QNS";
		public static string LowDnaResult = "Low gDNA";
		public static string HighCVResult = "High %CV";
		public static string LowFamFozResult = "LowFamFoz";
		public static string Unsatisfactory = "Unsatisfactory";
        public static string InsuficientDNA = "Insufficient DNA to perform analysis";        

		public static string CytologyReportNoPlaceHolder = "cytology_accessionno";

        public static string TestInformation = "The Aptima HPV assay is an in vitro nucleic acid amplification test for the qualitative detection of E6/E7 viral " +
            "messenger RNA (mRNA) from 14 high-risk types of human papillomavirus (HPV) in cervical specimens. The high-risk HPV types detected by the assay " +
            "include: 16, 18, 31, 33, 35, 39, 45, 51, 52, 56, 58, 59, 66, and 68. The Aptima HPV assay does not discriminate between the 14 high-risk types. " +
            "Cervical specimens in ThinPrep Pap Test vials containing PreservCyt Solution and collected with broom-type or cytobrush/spatula collection devices* " +
            "may be tested with the Aptima HPV assay. The assay is used with the Panther System. The use of the test is indicated: 1. To screen women 21 years and " +
            "older with atypical squamous cells of undetermined significance (ASC-US) cervical cytology results to determine the need for referral to colposcopy. The " +
            "results of this test are not intended to prevent women from proceeding to colposcopy. 2. In women 30 years and older, the Aptima HPV assay can be used with " +
            "cervical cytology to adjunctively screen to assess the presence or absence of high-risk HPV types. This information, together with the physician's " +
            "assessment of cytology history, other risk factors, and professional guidelines, may be used to guide patient management. This assay is not intended for use " +
            "as a screening device for women under age 30 with normal cervical " +
            "cytology. The Aptima HPV assay is not intended to substitute for regular cervical cytology screening. Detection of HPV using the Aptima HPV assay does not " +
            "differentiate HPV types and cannot evaluate persistence of any one type. The use of this assay has not been evaluated for the management of HPV vaccinated " +
            "women, women with prior ablative or excisional therapy, hysterectomy, who are pregnant, or who have other risk factors (e.g., HIV +, immunocompromised, " +
            "history of sexually transmitted infection).The Aptima HPV assay is designed to enhance existing methods for the detection of cervical disease and should be " +
            "used in conjunction with clinical information derived from other diagnostic and screening tests, physical examinations, and full medical history in " +
            "accordance with appropriate patient management procedures.";

        public static string References = "1. Darragh TM, Colgan TJ, Cox JT et al. The Lower Anogenital Squamous Terminology (LAST) Standardization Project for " +
            "HPV-Associated Lesions: Background and Consensus Recommendations from the College of American Pathologists and the American Society for Colposcopy and " +
            "Cervical Pathology. Arch Pathol Lab Med 2012 Oct; 136(10): 1266-97." + Environment.NewLine +
            "2.Doorbar, J. 2006. Molecular biology of human papillomavirus infection and cervical cancer.Clin Sci(Lond). 110(5):525-41.";

        public static string ASRComment = "This test was performed using a US FDA approved RNA probe kit.  The procedure and performance were verified by Yellowstone Pathology Institute (YPI).";

		public static string InsufficientComment = "The quantity of genomic DNA present in the sample is insufficient to perform the analysis, even after an attempt to " +
			"increase DNA content by using more specimen volume.  There is no charge for this specimen.  Consider repeat testing, if clinically indicated.";
		
		public static string IndeterminateComment = "Results are indeterminate due to technical issues with this specific specimen, which may be related to specimen DNA " +
			"quality or interfering substances.  Consider repeat testing, if clinically indicated.";

		public HPVResult()
		{
			
		}
	}
}
