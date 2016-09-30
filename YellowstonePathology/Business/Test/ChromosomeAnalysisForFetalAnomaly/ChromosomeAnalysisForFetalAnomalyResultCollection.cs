using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysisForFetalAnomaly
{
	public class ChromosomeAnalysisForFetalAnomalyResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public ChromosomeAnalysisForFetalAnomalyResultCollection()
		{
			this.Add(new ChromosomeAnalysisForFetalAnomalyNormalResult());
			this.Add(new ChromosomeAnalysisForFetalAnomalyAbnormalResult());
            this.Add(new ChromosomeAnalysisForFetalAnomalyNoGrowthResult());
            this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
