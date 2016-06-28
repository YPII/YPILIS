using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ZAP70LymphoidPanel
{
	public class ZAP70LymphoidPanelPositiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public ZAP70LymphoidPanelPositiveResult()
		{
			this.m_Result = "Positive";
			this.m_ResultCode = "ZAP70LMPHDPNLPOS";
		}
	}
}
