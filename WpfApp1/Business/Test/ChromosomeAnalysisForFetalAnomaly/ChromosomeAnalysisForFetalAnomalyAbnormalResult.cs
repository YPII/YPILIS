using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	public class ChromosomeAnalysisForFetalAnomalyAbnormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public ChromosomeAnalysisForFetalAnomalyAbnormalResult()
		{
			this.m_Result = "Abnormal";
			this.m_ResultCode = "CHRMSMNLSSFRFTLNMLABNRML";
		}
	}
}
