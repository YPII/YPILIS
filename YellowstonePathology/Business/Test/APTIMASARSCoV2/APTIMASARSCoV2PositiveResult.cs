using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	public class APTIMASARSCoV2PositiveResult : APTIMASARSCoV2Result
	{
		public APTIMASARSCoV2PositiveResult()
		{            
            this.m_ResultCode = "SARSCOV2PSTV";			                     
			this.m_Result = APTIMASARSCoV2Result.POSITIVE;            
		}
	}
}
