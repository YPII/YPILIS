using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class MLH1MethylationAnalysisNotSetResult : MLH1MethylationAnalysisResult
	{
		public MLH1MethylationAnalysisNotSetResult()
		{
			this.m_Result = "Not Set";
            this.m_ResultCode = "MLH1NTST";			
		}
	}
}
