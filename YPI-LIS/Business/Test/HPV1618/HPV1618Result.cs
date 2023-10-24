using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618Result : YellowstonePathology.Business.Test.TestResult
	{
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string InvalidResult = "Invalid";
        public static string InvalidResultCode = "HPV1618NVLD";

        public static string Method = "The Aptima HPV 16 18/45 genotype assay is an in vitro nucleic acid amplification test for the " +
            "qualitative detection of E6/E7 viral messenger RNA(mRNA) of human papillomavirus(HPV) " +
            "types 16, 18, and 45 in cervical specimens from women with Aptima HPV assay positive results. " +
            "The Aptima HPV 16 18/45 genotype assay can differentiate HPV 16 from HPV 18 and/or HPV 45, " +
            "but does not differentiate between HPV 18 and HPV 45. Cervical specimens in ThinPrep Pap " +
            "Test vials containing PreservCyt Solution and collected with broom-type or cytobrush/spatula " +
            "collection devices may be tested with the Aptima HPV 16 18/45 genotype assay. ";

        public static string References = "Khan, M.J., P.E. Castle, A.T. Lorincz, S. Wacholder, M. Sherman, D.R. Scott, B.B. Rush, A.G. Glass and M. Schiffman. 2005. " +
            "The elevated 10-year risk of cervical precancer and cancer in women with human papillomavirus (HPV) type 16 or 18 and the possible utility of type-specific HPV testing in " +
            "clinical practice. J. Natl. Cancer Inst. 97(14): 1072-1079. Burd, E.M. 2003. Human papillomavirus and cervical cancer. Clin Microbiol Rev. 16(1):1-17.";
        
        protected string m_HPV16Result;
        protected string m_HPV18Result;
        protected string m_m_HPV16ResultCode;
        protected string m_m_HPV18ResultCode;
        protected string m_Method;
        protected string m_References;

        public HPV1618Result()
		{
            this.m_Method = HPV1618Result.Method;
            this.m_References = HPV1618Result.References;
        }
	}
}
