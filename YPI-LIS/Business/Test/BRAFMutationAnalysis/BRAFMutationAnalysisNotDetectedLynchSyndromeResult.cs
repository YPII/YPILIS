using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisNotDetectedLynchSyndromeResult : BRAFMutationAnalysisNotDetectedResult
    {
        public BRAFMutationAnalysisNotDetectedLynchSyndromeResult()
        {
            YellowstonePathology.Business.Test.IndicationLynchSymdrome indication = new IndicationLynchSymdrome();
            this.m_Indication = indication.IndicationCode;
            this.m_IndicationComment = indication.Description;
            this.m_Interpretation = "The BRAF V600E status was tested on this colon tumor to rule out Lynch syndrome.  Immunohistochemistry for MLH1 " +
            "and PMS2 showed loss of these proteins in the colon tumor.  This can be either due to sporadic MLH1 methylation in the tumor or be related to a mutation in " +
            "MLH1 associated with Lynch syndrome.  BRAF mutations are highly correlated with sporadic MLH1 methylation and have not been seen in Lynch associated colon " +
            "cancers.  Therefore, the BRAF gene in the tumor was tested.  The result is negative for BRAF V600E mutation in the tumor. The tumor should be tested for MLH1 " +
            "methylation to rule out Lynch syndrome.";
            this.m_Method = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult.Method;
            this.m_Comment = "Recommend testing for MLH1 methylation.";
            this.m_References = YellowstonePathology.Business.Test.LynchSyndrome.LSERule.LSEColonReferences;
        }
    }
}