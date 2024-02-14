using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish
{
	public class MultipleMyelomaMGUSByFishAbnormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public MultipleMyelomaMGUSByFishAbnormalResult()
		{
			this.m_Result = "Abnormal";
			this.m_ResultCode = "MLTPLMLMMGUSByFSHABNRML";
		}
	}
}
