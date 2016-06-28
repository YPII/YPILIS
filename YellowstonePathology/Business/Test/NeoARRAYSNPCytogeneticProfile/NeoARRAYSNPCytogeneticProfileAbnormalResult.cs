using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile
{
	public class NeoARRAYSNPCytogeneticProfileAbnormalResult : YellowstonePathology.Business.Test.TestResult
	{
		public NeoARRAYSNPCytogeneticProfileAbnormalResult()
		{
			this.m_Result = "Abnormal";
			this.m_ResultCode = "NEORRSNPCTGNTCPRFLABNRNL";
		}
	}
}
