using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class GFAP : ImmunoHistochemistryTest
	{
		public GFAP()
		{
			this.m_TestId = 104;
			this.m_TestName = "GFAP";
            this.m_TestAbbreviation = "GFAP";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
