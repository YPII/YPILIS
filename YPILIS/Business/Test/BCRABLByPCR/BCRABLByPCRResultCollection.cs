using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
	public class BCRABLByPCRResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public BCRABLByPCRResultCollection()
		{
			this.Add(new BCRABLByPCRNotDetectedResult());
			this.Add(new BCRABLByPCRDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
