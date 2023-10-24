using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NRASMutationAnalysis
{
	public class NRASMutationAnalysisNotDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public NRASMutationAnalysisNotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ResultCode = "NRASMTTNNTDTCTD";
		}
	}
}
