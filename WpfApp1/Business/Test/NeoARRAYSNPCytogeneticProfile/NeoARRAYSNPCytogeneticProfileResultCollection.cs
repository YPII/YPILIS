using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NeoARRAYSNPCytogeneticProfile
{
	public class NeoARRAYSNPCytogeneticProfileResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public NeoARRAYSNPCytogeneticProfileResultCollection()
		{
			this.Add(new NeoARRAYSNPCytogeneticProfileNormalResult());
			this.Add(new NeoARRAYSNPCytogeneticProfileAbnormalResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
