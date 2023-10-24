using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
    public class SARSCoV2InvalidResult : SARSCoV2Result
	{
		public SARSCoV2InvalidResult()
		{
            this.m_ResultCode = "SRSCV2INVLD";
            this.m_Result = SARSCoV2Result.InvalidResult;                        
        }
	}
}
