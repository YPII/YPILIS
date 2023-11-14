using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV16PositiveHPV18NegativeResult : HPV1618ByPCRResult
	{
        public static string SquamousCellCarcinomaInterpretation = "About 30% of squamous cell carcinomas arising in the head and neck are driven by infection " +
            "by human papillomavirus (HPV) type 16 and 18.  These tumors are biologically and clinically distinct from non-HPV related tumors, typically occurring in " +
            "younger patients and without an association with alcohol or tobacco.   Multiple studies have shown that HPV-positive tumors have a better prognosis; there " +
            "is slower disease progression and improved survival with as much as a 60%-80% reduction in risk of death due to the disease.  Further, there may be differences " +
            "in the management of patients with an HPV-related malignancy.  This patient was diagnosed with a squamous cell carcinoma of the head and neck and was " +
            "therefore referred for testing for HPV type 16/18.  HPV 16 was detected in the sample by real time PCR.  HPV 18 was not detected.";

        public HPV16PositiveHPV18NegativeResult()
        {
            this.m_ResultCode = "HPV1618PN";
            this.m_HPV16Result = HPV1618ByPCRResult.PositiveResult;
            this.m_HPV18Result = HPV1618ByPCRResult.NegativeResult;
            this.m_SquamousCellCarcinomaInterpretation = SquamousCellCarcinomaInterpretation;
        }
	}
}
