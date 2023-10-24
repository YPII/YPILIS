using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish
{
	public class MultipleMyelomaMGUSByFishInconclusiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public MultipleMyelomaMGUSByFishInconclusiveResult()
		{
			this.m_Result = "Inconclusive";
            this.m_ResultCode = "MLTPLMLMMGUSByFSHNCNCLSV";            
		}
	}
}
