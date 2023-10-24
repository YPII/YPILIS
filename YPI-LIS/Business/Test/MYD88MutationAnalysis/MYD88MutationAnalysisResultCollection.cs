using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MYD88MutationAnalysis
{
	public class MYD88MutationAnalysisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public MYD88MutationAnalysisResultCollection()
		{
			this.Add(new MYD88MutationAnalysisDetectedResult());
			this.Add(new MYD88MutationAnalysisNotDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
