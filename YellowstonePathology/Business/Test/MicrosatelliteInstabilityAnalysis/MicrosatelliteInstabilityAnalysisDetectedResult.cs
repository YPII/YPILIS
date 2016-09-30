using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis
{
	public class MicrosatelliteInstabilityAnalysisDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public MicrosatelliteInstabilityAnalysisDetectedResult()
		{
			this.m_Result = "Detected";
			this.m_ResultCode = "MCRSTLLTNSTBLTYDTCTD";
		}
	}
}
