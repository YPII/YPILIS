using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
	public class BCRABLByPCRNotDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public BCRABLByPCRNotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ResultCode = "BCRABLPCRNTDTCTD";
		}
	}
}
