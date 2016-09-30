using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD56 : ImmunoHistochemistryTest
	{
		public CD56()
		{
			this.m_TestId = 77;
			this.m_TestName = "CD56";
            this.m_TestAbbreviation = "CD56";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
