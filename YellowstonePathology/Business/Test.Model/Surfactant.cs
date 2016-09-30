using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Surfactant : ImmunoHistochemistryTest
	{
		public Surfactant()
		{
			this.m_TestId = 171;
			this.m_TestName = "Surfactant";
            this.m_TestAbbreviation = "Surfactant";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
