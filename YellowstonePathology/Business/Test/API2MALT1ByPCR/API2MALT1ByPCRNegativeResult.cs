using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.API2MALT1ByPCR
{
    public class API2MALT1ByPCRNegativeResult : YellowstonePathology.Business.Test.TestResult
    {
        public API2MALT1ByPCRNegativeResult()
        {
            this.m_Result = "Negative";
            this.m_ResultCode = "P2MLT1BFSHNGTV";
        }
    }
}
