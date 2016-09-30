using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class EGFR : ImmunoHistochemistryTest
	{
		public EGFR()
		{
			this.m_TestId = 95;
			this.m_TestName = "EGFR";
            this.m_TestAbbreviation = "EGFR";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
