using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ZAP70LymphoidPanel
{
	public class ZAP70LymphoidPanelResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public ZAP70LymphoidPanelResultCollection()
		{
			this.Add(new ZAP70LymphoidPanelPositiveResult());
			this.Add(new ZAP70LymphoidPanelNegaiveResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
