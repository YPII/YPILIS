using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
    public class PDL1NegativeResult : TestResult
    {
        public PDL1NegativeResult()
        {
            this.m_Result = "Negative";
            this.m_ResultCode = "PDL1NGTV";
        }
    }
}
