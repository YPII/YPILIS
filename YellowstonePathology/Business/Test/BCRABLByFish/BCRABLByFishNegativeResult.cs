using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByFish
{
	public class BCRABLByFishNegativeResult : YellowstonePathology.Business.Test.TestResult
	{
		public BCRABLByFishNegativeResult()
		{
			this.m_Result = "Negative";
			this.m_ResultCode = "BCRABLFSHNGTV";
		}
	}
}
