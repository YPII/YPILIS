using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.API2MALT1ByPCR
{
    public class API2MALT1ByPCRPositiveResult : YellowstonePathology.Business.Test.TestResult
    {
        public API2MALT1ByPCRPositiveResult()
        {
            this.m_Result = "Positive";
            this.m_ResultCode = "P2MLT1BFSHPSTV";
        }
    }
}
