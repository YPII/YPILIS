using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ALK1 : ImmunoHistochemistryTest
	{
		public ALK1()
		{
			this.m_TestId = 52;
			this.m_TestName = "ALK-1";
            this.m_TestAbbreviation = "ALK-1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
