using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ABL1KinaseDomainMutation
{
	public class ABL1KinaseDomainMutationNotDetectedResult : YellowstonePathology.Business.Test.TestResult
	{
		public ABL1KinaseDomainMutationNotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ResultCode = "ABL1KNSMTTNNTDTCTD";
		}
	}
}
