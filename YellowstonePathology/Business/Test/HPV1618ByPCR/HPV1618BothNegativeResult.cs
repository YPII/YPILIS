using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618BothNegativeResult : HPV1618ByPCRResult
	{
        public static string SquamousCellCarcinomaInterpretation = "About 30% of squamous cell carcinomas arising in the head and neck are driven by infection by human papillomavirus (HPV) type 16/18.  " +
            "These tumors are biologically and clinically distinct from non-HPV related tumors, typically occurring in younger patients and without an association " +
            "with alcohol or tobacco.   Multiple studies have shown that HPV-positive tumors have a better prognosis; there is slower disease progression and improved " +
            "survival with as much as a 60%-80% reduction in risk of death due to the disease.  Further, there may be differences in the management of patients with an " +
            "HPV-related malignancy.  This patient was diagnosed with a squamous cell carcinoma of the head and neck and was therefore referred for testing for HPV type 16/18.  " +
            "During the PCR reaction on this patient's sample there was no fluorescence signal detected in the pattern of a classic PCR curve and exceededing the threshold " +
            "determined during test validation to be significant for the presence of HPV-16/18.  All controls including the sample's internal control performed acceptably.  " +
            "Therefore HPV-16/18 is not detected in this patient's tissue, although the presence of other high-risk HPV types cannot be excluded.";        

        public HPV1618BothNegativeResult()
        {
            this.m_ResultCode = "HPV1618NN";
            this.m_HPV16Result = HPV1618ByPCRResult.NegativeResult;
            this.m_HPV18Result = HPV1618ByPCRResult.NegativeResult;
            this.m_SquamousCellCarcinomaInterpretation = SquamousCellCarcinomaInterpretation;
        }
	}
}
