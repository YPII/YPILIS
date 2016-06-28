using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TouchPrep : NoCptCodeTest
	{
		public TouchPrep()
		{
			this.m_TestId = 196;
			this.m_TestName = "Touch Prep";
            this.m_TestAbbreviation = "Touch Prep";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
