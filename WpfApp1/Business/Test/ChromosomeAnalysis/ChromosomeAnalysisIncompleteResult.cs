using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisIncompleteResult : YellowstonePathology.Business.Test.TestResult
	{
        public ChromosomeAnalysisIncompleteResult()
		{
			this.m_Result = "Incomplete";
			this.m_ResultCode = "CHRMSMNLYSNCMPLT";
		}
	}
}