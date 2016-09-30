using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD3CD20DualStain : DualStain
	{
		public CD3CD20DualStain()
		{
            this.m_TestId = "CD30CD20";
			this.m_TestName = "CD3/CD20";
			this.m_FirstTest = new CD3();
			this.m_SecondTest = new CD20();

			this.m_DepricatedFirstTestId = 229;
			this.m_DepricatedSecondTestId = 230;
		}
	}
}
