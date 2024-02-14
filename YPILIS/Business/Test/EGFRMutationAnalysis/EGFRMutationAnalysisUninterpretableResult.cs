using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisUninterpretableResult : EGFRMutationAnalysisResult
    {
        public EGFRMutationAnalysisUninterpretableResult()
        {
            this.m_ResultCode = "60020";   
            this.m_ReflexToALKIsIndicated = false;
            this.m_ReflexToALKComment = "Reflexing an uninterpretable EGFR result to ALK is not necessary.";
            this.m_Result = "Uninterpretable";
            this.m_Comment = null;
            this.m_Interpretation = null;
        }        
    }
}
