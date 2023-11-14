using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByFish
{
	public class BCRABLByFishPositiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public BCRABLByFishPositiveResult()
		{
			this.m_Result = "Positive";
			this.m_ResultCode = "BCRABLFSHPSTV";
		}
	}
}
