using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
	public class BCRABLByPCRDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public BCRABLByPCRDetectedResult()
		{
			this.m_Result = "Detected";
			this.m_ResultCode = "BCRABLPCRDTCTD";
		}
	}
}
