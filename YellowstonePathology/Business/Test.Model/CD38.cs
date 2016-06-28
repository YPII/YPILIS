using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD38 : ImmunoHistochemistryTest
	{
		public CD38()
		{
			this.m_TestId = 179;
			this.m_TestName = "CD38";
            this.m_TestAbbreviation = "CD38";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
