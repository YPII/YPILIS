using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NRASMutationAnalysis
{
	public class NRASMutationAnalysisDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public NRASMutationAnalysisDetectedResult()
		{
			this.m_Result = "Detected";
			this.m_ResultCode = "NRASMTTNDTCTD";
		}
	}
}
