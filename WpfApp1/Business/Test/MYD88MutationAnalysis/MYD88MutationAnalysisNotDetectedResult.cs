using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MYD88MutationAnalysis
{
	public class MYD88MutationAnalysisNotDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public MYD88MutationAnalysisNotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ResultCode = "MYD88MTTNNTDTCTD";
		}
	}
}
