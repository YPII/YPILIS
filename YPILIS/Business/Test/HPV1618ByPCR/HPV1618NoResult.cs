using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618NoResult : HPV1618ByPCRResult
	{
        public HPV1618NoResult()
        {
            this.m_ResultCode = null;
            this.m_HPV16Result = null;
            this.m_HPV18Result = null;
            this.m_SquamousCellCarcinomaInterpretation = null;
            this.m_References = null;
            this.m_Method = null;
        }
	}
}
