using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BladderCancerFISHUrovysion
{
	public class BladderCancerFISHUrovysionNegativeResult : YellowstonePathology.Business.Test.TestResult
	{
		public BladderCancerFISHUrovysionNegativeResult()
		{
			this.m_Result = "Negative";
			this.m_ResultCode = "BLDDRCNCRFSHRVSNNGTV";
		}
	}
}
