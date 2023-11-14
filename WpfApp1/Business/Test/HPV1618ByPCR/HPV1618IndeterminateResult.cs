using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618IndeterminateResult : HPV1618ByPCRResult
	{
        public HPV1618IndeterminateResult()
        {
            this.m_ResultCode = "HPV1618INDMT";
            this.m_HPV16Result = HPV1618ByPCRResult.IndeterminateResult;
            this.m_HPV18Result = HPV1618ByPCRResult.IndeterminateResult;
        }
	}
}
