using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD10 : ImmunoHistochemistryTest
	{
		public CD10()
		{
			this.m_TestId = 61;
			this.m_TestName = "CD10";
            this.m_TestAbbreviation = "CD10";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
