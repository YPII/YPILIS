using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	public class APTIMASARSCoV2NegativeResult : APTIMASARSCoV2Result
	{
		public APTIMASARSCoV2NegativeResult()
		{            
            this.m_ResultCode = "SARSCOV2NGTV";
            this.m_Result = APTIMASARSCoV2Result.NEGATIVE;            
		}
	}
}
