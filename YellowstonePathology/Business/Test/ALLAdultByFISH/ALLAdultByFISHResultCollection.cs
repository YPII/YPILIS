using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALLAdultByFISH
{
	public class ALLAdultByFISHResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public ALLAdultByFISHResultCollection()
		{
			this.Add(new ALLAdultByFISHNormalResult());
			this.Add(new ALLAdultByFISHAbnormalRsult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
