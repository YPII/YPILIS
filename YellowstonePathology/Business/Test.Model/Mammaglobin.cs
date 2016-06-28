using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Mammaglobin : ImmunoHistochemistryTest
	{
		public Mammaglobin()
		{
			this.m_TestId = 118;
			this.m_TestName = "Mammaglobin";
            this.m_TestAbbreviation = "Mammaglobin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
