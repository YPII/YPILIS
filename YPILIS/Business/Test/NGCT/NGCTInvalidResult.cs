using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTInvalidResult : NGCTResult
    {
        public NGCTInvalidResult()
        {
            this.m_ResultCode = NGCTResult.InvalidResultCode;
            this.m_Result = NGCTResult.InvalidResult;
        }
    }
}
