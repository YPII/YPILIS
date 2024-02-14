using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisResultFactory
    {
        public static EGFRMutationAnalysisResult GetResult(string resultCode)
        {
            EGFRMutationAnalysisResult result = null;
            switch (resultCode)
            {
                case "60001":
                    result = new EGFRMutationAnalysisNotDetectedResult();
                    break;
                case "60002":
                    result = new EGFRMutationAnalysisDetectedResult();
                    break;
                case "60010":
                    result = new EGFRMutationAnalysisResistanceResult();
                    break;
                case "60020":
                    result = new EGFRMutationAnalysisUninterpretableResult();
                    break;
            }
            return result;
        }
    }
}
