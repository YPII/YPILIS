using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisNotDetectedResult : EGFRMutationAnalysisResult
    {
        public EGFRMutationAnalysisNotDetectedResult()
        {
            this.m_ResultCode = "60001";
            this.m_ReflexToALKIsIndicated = true;
            this.m_ReflexToALKComment = "An EGFR result of Not Detected may indicate that ALK should be ordered.";
            this.m_Result = "EGFR Mutations not detected.";
            this.m_ResultAbbreviation = "Not Detected";
            this.m_Comment = "This result indicates the tumor has decreased sensitivity to EGFR TKIs.";
            this.m_Interpretation = "Screening for EGFR mutations may help identify patients with tumors that will benefit from the use of EGFR tyrosine kinase " +
                "inhibitors (TKIs).  Mutations may be activating and indicate responsiveness to TKIs or it may be a resistance mutation indicating a lack of response to TKIs.  " +
                "A product indicative of an EGFR mutation was not detected.  This indicates the tumor has a decreased sensitivity to TKI therapy. ";
        }        
    }
}
