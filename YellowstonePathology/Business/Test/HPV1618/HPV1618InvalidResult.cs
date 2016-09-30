using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618InvalidResult : HPV1618Result
	{
        public HPV1618InvalidResult()
        {
            this.m_ResultCode = HPV1618Result.InvalidResultCode;
            this.m_Result = HPV1618Result.InvalidResult;
        }
	}
}
