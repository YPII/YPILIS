using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class BartonellaHenselae : ImmunoHistochemistryTest
	{
		public BartonellaHenselae()
		{
			this.m_TestId = 168;
			this.m_TestName = "Bartonella henselae (Cat Scratch)";
            this.m_TestAbbreviation = "Cat Scratch";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
