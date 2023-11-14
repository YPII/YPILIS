using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR
{
    public class CCNDIBCLIGHByPCRAbnormalResult : CCNDIBCLIGHByPCRResult
    {
        public CCNDIBCLIGHByPCRAbnormalResult()
        {
            this.m_Result = "Not Detected";
            this.m_ResultCode = "CCNDIBCLIGHBPCRBMRNL";
            this.m_References = null;
        }
    }
}
