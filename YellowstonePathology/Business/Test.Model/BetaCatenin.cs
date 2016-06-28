using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class BetaCatenin : ImmunoHistochemistryTest
	{
		public BetaCatenin()
		{
			this.m_TestId = 299;
			this.m_TestName = "Beta-Catenin";
            this.m_TestAbbreviation = "Beta-Catenin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
