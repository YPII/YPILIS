using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BladderCancerFISHUrovysion
{
	public class BladderCancerFISHUrovysionPositiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public BladderCancerFISHUrovysionPositiveResult()
		{
			this.m_Result = "Positive";
			this.m_ResultCode = "BLDDRCNCRFSHRVSNPSTV";
		}
	}
}
