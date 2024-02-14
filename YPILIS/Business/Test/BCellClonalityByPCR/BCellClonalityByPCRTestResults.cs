using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	public class BCellClonalityByPCRTestResults :List<string>
	{
		public BCellClonalityByPCRTestResults()
		{
			this.Add("Not Clonal");
			this.Add("Clonal");
			this.Add("Indeterminate");
			this.Add(string.Empty);
		}
	}
}
