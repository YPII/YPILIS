using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisNotDetectedResult : TestResult
    {
        public BRAFMutationAnalysisNotDetectedResult()
        {
            this.m_ResultCode = "BRAFMTTNANLNTDTCTD";
            this.m_Result = TestResult.NotDetected;
        }
    }
}
