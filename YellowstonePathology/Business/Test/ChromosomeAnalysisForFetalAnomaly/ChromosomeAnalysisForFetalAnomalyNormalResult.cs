using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	public class ChromosomeAnalysisForFetalAnomalyNormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public ChromosomeAnalysisForFetalAnomalyNormalResult()
		{
			this.m_Result = "Normal";
			this.m_ResultCode = "CHRMSMNLSSFRFTLNMLNRML";
		}
	}
}
