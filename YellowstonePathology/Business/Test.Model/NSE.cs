using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NSE : ImmunoHistochemistryTest
	{
		public NSE()
		{
			this.m_TestId = 130;
			this.m_TestName = "NSE";
            this.m_TestAbbreviation = "NSE";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
