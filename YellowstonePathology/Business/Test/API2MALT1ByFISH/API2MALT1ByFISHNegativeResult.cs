using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.API2MALT1ByFISH
{
	public class API2MALT1ByFISHNegativeResult : YellowstonePathology.Business.Test.TestResult
	{
		public API2MALT1ByFISHNegativeResult()
		{
			this.m_Result = "Negative";
			this.m_ResultCode = "P2MLT1NGTV";
		}
	}
}
