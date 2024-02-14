using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisIndeterminateResult : YellowstonePathology.Business.Test.TestResult
	{
        public ChromosomeAnalysisIndeterminateResult()
		{
			this.m_Result = "Indeterminate";
			this.m_ResultCode = "CHRMSMNLYSNDTRMNT";
		}
	}
}
