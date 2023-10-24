using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.DeletionsForGlioma1p19q
{
	public class DeletionsForGlioma1p19qResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public DeletionsForGlioma1p19qResultCollection()
		{
			this.Add(new DeletionsForGlioma1p19qNegativeResult());
			this.Add(new DeletionsForGlioma1p19qPositiveResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
