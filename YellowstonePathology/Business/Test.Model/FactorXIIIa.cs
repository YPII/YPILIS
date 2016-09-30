using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class FactorXIIIa : ImmunoHistochemistryTest
	{
		public FactorXIIIa()
		{
			this.m_TestId = 101;
			this.m_TestName = "Factor XIIIa";
            this.m_TestAbbreviation = "Factor XIIIa";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
