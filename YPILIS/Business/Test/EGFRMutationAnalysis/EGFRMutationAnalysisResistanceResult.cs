using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisResistanceResult : EGFRMutationAnalysisResult
    {
        public EGFRMutationAnalysisResistanceResult()
        {
            this.m_ResultCode = "60010";
            this.m_ReflexToALKIsIndicated = false;
            this.m_ReflexToALKComment = "Reflexing a resistant EGFR result to ALK is not necessary.";            
            this.m_Result = "Resistance Mutation Detected (*MUTATION*)";
            this.m_ResultAbbreviation = "Resistance Detected";
            this.m_Comment = "The *MUTATION* mutation is associated with decreased sensitivity to TKIs.";
            this.m_Interpretation = "Screening for EGFR mutations may help identify patients with tumors that will benefit from the use of EGFR tyrosine " +
                "kinase inhibitors (TKIs).  Mutations may be activating and indicate responsiveness to TKIs or it may be a resistance mutation indicating a lack of response to TKIs.  " +
                "A product indicative of a resistance EGFR mutation was detected.  This indicates the tumor has a decreased sensitivity to TKI therapy. ";
        }

        public override void SetResult(EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder, string mutation)
        {
            base.SetResult(egfrMutationAnalysisTestOrder, mutation);
            egfrMutationAnalysisTestOrder.Result = this.m_Result.Replace("*MUTATION*", mutation);
            egfrMutationAnalysisTestOrder.Comment = this.m_Comment.Replace("*MUTATION*", mutation);
        }
    }
}
