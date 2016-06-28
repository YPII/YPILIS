using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MSH6 : ImmunoHistochemistryTest
	{
		public MSH6()
		{
			this.m_TestId = 218;
			this.m_TestName = "MSH6";
            this.m_TestAbbreviation = "MSH6";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
