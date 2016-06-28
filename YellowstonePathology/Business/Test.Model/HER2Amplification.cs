using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HER2Amplification : NoCptCodeTest
	{
		public HER2Amplification()
		{
			this.m_TestId = 190;
			this.m_TestName = "HER2 Amplification";
            this.m_TestAbbreviation = "HER2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
