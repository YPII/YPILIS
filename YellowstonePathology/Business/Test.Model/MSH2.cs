using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MSH2 : ImmunoHistochemistryTest
	{
		public MSH2()
		{
			this.m_TestId = 122;
			this.m_TestName = "MSH2";
            this.m_TestAbbreviation = "MSH2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
