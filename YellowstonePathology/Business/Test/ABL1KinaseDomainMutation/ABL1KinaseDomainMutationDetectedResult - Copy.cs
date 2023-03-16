using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ABL1KinaseDomainMutation
{
	public class ABL1KinaseDomainMutationDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public ABL1KinaseDomainMutationDetectedResult()
		{
			this.m_Result = "Detected";
			this.m_ResultCode = "ABL1KNSMTTNDTCTD";
		}
	}
}
