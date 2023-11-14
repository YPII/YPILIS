using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MYD88MutationAnalysis
{
	public class MYD88MutationAnalysisDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public MYD88MutationAnalysisDetectedResult()
		{
			this.m_Result = "Detected";
			this.m_ResultCode = "MYD88MTTNDTCTD";
		}
	}
}
