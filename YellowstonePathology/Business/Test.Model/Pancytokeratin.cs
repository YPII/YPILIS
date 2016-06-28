using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Pancytokeratin : ImmunoHistochemistryTest
	{
		public Pancytokeratin()
		{
			this.m_TestId = 136;
			this.m_TestName = "Pancytokeratin";
            this.m_TestAbbreviation = "Pancytokeratin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
