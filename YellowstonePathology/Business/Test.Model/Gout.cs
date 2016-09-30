using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Gout : CytochemicalForMicroorganisms
	{
        public Gout()
		{
			this.m_TestId = 358;
			this.m_TestName = "Gout";
            this.m_TestAbbreviation = "Gout";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
