using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Bcl6 : ImmunoHistochemistryTest
	{
		public Bcl6()
		{
			this.m_TestId = 57;
			this.m_TestName = "Bcl-6";
            this.m_TestAbbreviation = "Bcl-6";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
