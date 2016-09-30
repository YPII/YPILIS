using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis
{
	public class MicrosatelliteInstabilityAnalysisNotDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public MicrosatelliteInstabilityAnalysisNotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ResultCode = "MCRSTLLTNSTBLTYNOTDTCTD";
		}
	}
}
