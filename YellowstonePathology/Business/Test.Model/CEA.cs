using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CEA : ImmunoHistochemistryTest
	{
		public CEA()
		{
			this.m_TestId = 82;
			this.m_TestName = "CEA";
            this.m_TestAbbreviation = "CEA";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
