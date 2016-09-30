using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HER2DISH : ImmunoHistochemistryTest
	{
        public HER2DISH()
		{
			this.m_TestId = 999;
            this.m_TestName = "HER2 (D-ISH)";
            this.m_TestAbbreviation = "HER2 (D-ISH)";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
