using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class AcidFast : CytochemicalForMicroorganisms
	{
		public AcidFast()
		{
			this.m_TestId = 50;
			this.m_TestName = "Acid Fast";
            this.m_TestAbbreviation = "Acid Fast";
			this.m_Active = true;
            this.m_NeedsAcknowledgement = true;
		}
	}
}
