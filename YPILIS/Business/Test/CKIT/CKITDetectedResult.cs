using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CKIT
{
	public class CKITDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public CKITDetectedResult()
		{
			this.m_Result = "Detected";
			this.m_ResultCode = "CKITDTCTD";
		}
	}
}
