using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FactorVLeiden
{
	public class FactorVLeidenResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public FactorVLeidenResultCollection()
		{
			this.Add(new FactorVLeidenHeterozygousMutationDetectedResult());
			this.Add(new FactorVLeidenHomozygousMutationDetectedResult());
			this.Add(new FactorVLeidenNotDetectedResult());
			this.Add(new FactorVLeidenIndeterminateResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
