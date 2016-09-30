using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Tyrosinase : ImmunoHistochemistryTest
	{
		public Tyrosinase()
		{
			this.m_TestId = 162;
			this.m_TestName = "Tyrosinase";
            this.m_TestAbbreviation = "Tyrosinase";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
