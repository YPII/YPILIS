using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.API2MALT1ByFISH
{
	public class API2MALT1ByFISHPositiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public API2MALT1ByFISHPositiveResult()
		{
			this.m_Result = "Positive";
			this.m_ResultCode = "P2MLT1PSTV";
		}
	}
}
