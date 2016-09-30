using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Recut : NoCptCodeTest
	{
		public Recut()
		{
			this.m_TestId = 150;
			this.m_TestName = "Recut";
            this.m_TestAbbreviation = "Recut";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
