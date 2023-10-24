using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CLLByFish
{
	public class CLLByFishAbnormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public CLLByFishAbnormalResult()
		{
			this.m_Result = "Abnormal";
			this.m_ResultCode = "CLLBYFSHABNRML";
		}
	}
}
