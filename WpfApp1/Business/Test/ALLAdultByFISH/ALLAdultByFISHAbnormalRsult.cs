using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALLAdultByFISH
{
	public class ALLAdultByFISHAbnormalRsult : YellowstonePathology.Business.Test.TestResult
	{
		public ALLAdultByFISHAbnormalRsult()
		{
			this.m_Result = "Abnormal";
			this.m_ResultCode = "LLDLTBFSHABNRML";
		}
	}
}
