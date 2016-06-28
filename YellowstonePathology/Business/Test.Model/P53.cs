using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class P53 : ImmunoHistochemistryTest
	{
		public P53()
		{
			this.m_TestId = 134;
			this.m_TestName = "P53";
            this.m_TestAbbreviation = "P53";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
