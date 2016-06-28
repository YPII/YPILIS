using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ABL1KinaseDomainMutation
{
	public class ABL1KinaseDomainMutationResultCollection :YellowstonePathology.Business.Test.TestResultCollection
	{
		public ABL1KinaseDomainMutationResultCollection()
		{
			this.Add(new ABL1KinaseDomainMutationNotDetectedResult());
			this.Add(new ABL1KinaseDomainMutationDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
