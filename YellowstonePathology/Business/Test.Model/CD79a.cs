using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD79a : ImmunoHistochemistryTest
	{
		public CD79a()
		{
			this.m_TestId = 79;
			this.m_TestName = "CD79a";
            this.m_TestAbbreviation = "CD79a";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
