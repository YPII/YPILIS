using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ZAP70LymphoidPanel
{
	public class ZAP70LymphoidPanelNegaiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public ZAP70LymphoidPanelNegaiveResult()
		{
			this.m_Result = "Negative";
			this.m_ResultCode = "ZAP70LMPHDPNLNEG";
		}
	}
}
