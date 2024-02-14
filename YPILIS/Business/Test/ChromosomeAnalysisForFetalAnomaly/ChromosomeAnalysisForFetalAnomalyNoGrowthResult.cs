using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	public class ChromosomeAnalysisForFetalAnomalyNoGrowthResult : YellowstonePathology.Business.Test.TestResult
	{
		public ChromosomeAnalysisForFetalAnomalyNoGrowthResult()
		{
			this.m_Result = "No Growth";
			this.m_ResultCode = "CHRMSMNLSSFRFTLNMLNGRWTH";            
		}
	}
}
