using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class EMA : ImmunoHistochemistryTest
	{
		public EMA()
		{
			this.m_TestId = 97;
			this.m_TestName = "EMA";
            this.m_TestAbbreviation = "EMA";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
