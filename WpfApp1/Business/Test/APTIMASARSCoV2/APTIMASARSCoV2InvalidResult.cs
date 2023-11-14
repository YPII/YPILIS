using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	public class APTIMASARSCoV2InvalidResult : APTIMASARSCoV2Result
	{
		public APTIMASARSCoV2InvalidResult()
		{            
            this.m_ResultCode = "SARSCOV2NVLD";
            this.m_Result = APTIMASARSCoV2Result.InvalidResult;            
		}
	}
}
