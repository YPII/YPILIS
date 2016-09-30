using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisNormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public ChromosomeAnalysisNormalResult()
		{
			this.m_Result = "Normal";
			this.m_ResultCode = "CHRMSMNLYSNRML";
		}
	}
}
