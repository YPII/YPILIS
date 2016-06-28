using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisInconclusiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public ChromosomeAnalysisInconclusiveResult()
		{
			this.m_Result = "Inconclusive";
			this.m_ResultCode = "CHRMSMNLYSNCNCLSV";
		}
	}
}
