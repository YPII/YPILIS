using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public ChromosomeAnalysisResultCollection()
		{
			this.Add(new ChromosomeAnalysisNormalResult());
			this.Add(new ChromosomeAnalysisAbnormalResult());
            this.Add(new ChromosomeAnalysisIndeterminateResult());
            this.Add(new ChromosomeAnalysisIncompleteResult());
            this.Add(new ChromosomeAnalysisInconclusiveResult());
            this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
