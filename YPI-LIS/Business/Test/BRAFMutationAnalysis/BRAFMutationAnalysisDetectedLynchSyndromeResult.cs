using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisDetectedLynchSyndromeResult : BRAFMutationAnalysisDetectedResult
    {
        public BRAFMutationAnalysisDetectedLynchSyndromeResult()
        {
            YellowstonePathology.Business.Test.IndicationLynchSymdrome indication = new IndicationLynchSymdrome();
            this.m_Indication = indication.IndicationCode;
            this.m_IndicationComment = indication.Description;
            this.m_Interpretation = "BRAF mutation detected.";
            this.m_Method = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult.Method;
            this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
            this.m_References = YellowstonePathology.Business.Test.LynchSyndrome.LSERule.LSEColonReferences;
        }
    }
}