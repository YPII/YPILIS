using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Synaptophysin : ImmunoHistochemistryTest
	{
		public Synaptophysin()
		{
			this.m_TestId = 156;
			this.m_TestName = "Synaptophysin";
            this.m_TestAbbreviation = "Synaptophysin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
