using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class P63 : ImmunoHistochemistryTest
	{
		public P63()
		{
			this.m_TestId = 135;
			this.m_TestName = "P63";
            this.m_TestAbbreviation = "P63";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
