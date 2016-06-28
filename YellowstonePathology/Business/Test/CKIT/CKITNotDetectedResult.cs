using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CKIT
{
	public class CKITNotDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public CKITNotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ResultCode = "CKITNTDTCTD";
		}
	}
}
