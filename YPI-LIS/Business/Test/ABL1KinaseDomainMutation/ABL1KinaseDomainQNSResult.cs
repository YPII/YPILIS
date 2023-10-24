using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ABL1KinaseDomainMutation
{
	public class ABL1KinaseDomainQNSResult : YellowstonePathology.Business.Test.TestResult
	{
		public ABL1KinaseDomainQNSResult()
		{
			this.m_Result = "Quantity Not Sufficient";
			this.m_ResultCode = "ABL1KNSMTTNQNS";
		}
	}
}
