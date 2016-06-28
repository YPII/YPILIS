using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ProstaticAcidPhosphatase : ImmunoHistochemistryTest
	{
		public ProstaticAcidPhosphatase()
		{
			this.m_TestId = 147;
			this.m_TestName = "Prostatic Acid Phosphatase";
            this.m_TestAbbreviation = "Prostatic Acid Phosphatase";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
