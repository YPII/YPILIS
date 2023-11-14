using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BladderCancerFISHUrovysion
{
	public class BladderCancerFISHUrovysionResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public BladderCancerFISHUrovysionResultCollection()
		{
			this.Add(new BladderCancerFISHUrovysionNegativeResult());
			this.Add(new BladderCancerFISHUrovysionPositiveResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
