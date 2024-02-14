using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	public class ProthrombinResultCollection :YellowstonePathology.Business.Test.TestResultCollection
	{
		public ProthrombinResultCollection()
		{
			this.Add(new ProthrombinNotDetectedResult());
			this.Add(new ProthrombinHeterozygousMutationDetectedResult());
			this.Add(new ProthrombinHomozygousMutationDetectedResult());
			this.Add(new ProthrombinIndeterminateResult());
			this.Add(new ProthrombinInsufficientResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
