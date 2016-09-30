using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class SteinerandSteiner : CytochemicalForMicroorganisms
	{
		public SteinerandSteiner()
		{
			this.m_TestId = 155;
			this.m_TestName = "Steiner and Steiner";
            this.m_TestAbbreviation = "Steiner and Steiner";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
