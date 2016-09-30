using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HandE : NoCptCodeTest
	{
		public HandE()
		{
			this.m_TestId = 49;
			this.m_TestName = "H&E";
            this.m_TestAbbreviation = "H&E";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
