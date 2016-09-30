using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisDetectedResult : EGFRMutationAnalysisResult
    {
        public EGFRMutationAnalysisDetectedResult()
        {
            this.m_ResultCode = "60002";
            this.m_ReflexToALKIsIndicated = false;
            this.m_ReflexToALKComment = "Reflexing a positive EGFR result to ALK is not necessary.";                        
            this.m_Result = "Positive (*MUTATION*)";
            this.m_ResultAbbreviation = "Detected";
            this.m_Comment = "The *MUTATION* is associated with a positive response to TKI therapy.";
            this.m_Interpretation = "Screening for EGFR mutations may help identify patients with tumors that will benefit from the use of EGFR tyrosine kinase inhibitors (TKIs).  " + 
                "Mutations may be activating and indicate responsiveness to TKIs or it may be a resistance mutation indicating a lack of response to TKIs.  " +
                "A product indicative of an activating EGFR mutation was detected.  This type of mutation has been correlated with responsiveness to TKI therapy. ";       
        }

        public override void SetResult(EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder, string mutation)
        {
            base.SetResult(egfrMutationAnalysisTestOrder, mutation);
            egfrMutationAnalysisTestOrder.Result = this.m_Result.Replace("*MUTATION*", mutation);
            egfrMutationAnalysisTestOrder.Comment = this.m_Comment.Replace("*MUTATION*", mutation);
        }        
    }
}
