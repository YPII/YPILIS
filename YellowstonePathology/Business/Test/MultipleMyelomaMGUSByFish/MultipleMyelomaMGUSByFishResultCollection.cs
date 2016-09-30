using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish
{
	public class MultipleMyelomaMGUSByFishResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public MultipleMyelomaMGUSByFishResultCollection()
		{
			this.Add(new MultipleMyelomaMGUSByFishNormalResult());
			this.Add(new MultipleMyelomaMGUSByFishAbnormalResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
