using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PAX5Zap70DualStain : DualStain
	{
		public PAX5Zap70DualStain()
		{
            this.m_TestId = "PAX5ZP70";
			this.m_TestName = "PAX-5/ZAP 70";
			this.m_FirstTest = new PAX5();
			this.m_SecondTest = new Zap70();

			this.m_DepricatedFirstTestId = 235;
			this.m_DepricatedSecondTestId = 236;
		}
	}
}
