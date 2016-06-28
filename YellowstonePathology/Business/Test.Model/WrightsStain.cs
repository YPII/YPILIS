using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class WrightsStain : NoCptCodeTest
	{
		public WrightsStain()
		{
			this.m_TestId = 205;
			this.m_TestName = "Wrights Stain";
            this.m_TestAbbreviation = "Wrights Stain";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
