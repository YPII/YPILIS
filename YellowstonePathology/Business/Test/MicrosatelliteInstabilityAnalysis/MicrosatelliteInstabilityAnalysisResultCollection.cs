using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis
{
	public class MicrosatelliteInstabilityAnalysisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public MicrosatelliteInstabilityAnalysisResultCollection()
		{
			this.Add(new MicrosatelliteInstabilityAnalysisNotDetectedResult());
			this.Add(new MicrosatelliteInstabilityAnalysisDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
