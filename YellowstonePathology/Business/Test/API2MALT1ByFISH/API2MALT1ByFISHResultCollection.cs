using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.API2MALT1ByFISH
{
	public class API2MALT1ByFISHResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public API2MALT1ByFISHResultCollection()
		{
			this.Add(new API2MALT1ByFISHNegativeResult());
			this.Add(new API2MALT1ByFISHPositiveResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
