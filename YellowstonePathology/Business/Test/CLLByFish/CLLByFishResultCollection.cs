using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CLLByFish
{
	public class CLLByFishResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public CLLByFishResultCollection()
		{
			this.Add(new CLLByFishNormalResult());
			this.Add(new CLLByFishAbnormalResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
