using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisAbnormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public ChromosomeAnalysisAbnormalResult()
		{
			this.m_Result = "Abnormal";
			this.m_ResultCode = "CHRMSMNLYSABNRML";
		}
	}
}
