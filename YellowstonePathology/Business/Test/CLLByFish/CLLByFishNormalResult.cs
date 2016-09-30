using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CLLByFish
{
	public class CLLByFishNormalResult :YellowstonePathology.Business.Test.TestResult
	{
		public CLLByFishNormalResult()
		{
			this.m_Result = "Normal";
			this.m_ResultCode = "CLLBYFSHNRML";
		}
	}
}
