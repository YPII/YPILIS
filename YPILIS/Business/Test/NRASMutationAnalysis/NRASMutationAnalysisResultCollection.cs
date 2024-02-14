using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NRASMutationAnalysis
{
	public class NRASMutationAnalysisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public NRASMutationAnalysisResultCollection()
		{
			this.Add(new NRASMutationAnalysisDetectedResult());
			this.Add(new NRASMutationAnalysisNotDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
