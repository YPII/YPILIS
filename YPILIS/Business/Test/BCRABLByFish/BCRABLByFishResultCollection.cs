using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByFish
{
	public class BCRABLByFishResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public BCRABLByFishResultCollection()
		{
			this.Add(new BCRABLByFishNegativeResult());
			this.Add(new BCRABLByFishPositiveResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
