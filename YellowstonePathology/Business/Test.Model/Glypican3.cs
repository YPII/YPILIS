using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Glypican3 : ImmunoHistochemistryTest
	{
		public Glypican3()
		{
			this.m_TestId = 276;
			this.m_TestName = "Glypican 3";
            this.m_TestAbbreviation = "Glypican 3";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
