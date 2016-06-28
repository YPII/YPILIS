using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PMS2 : ImmunoHistochemistryTest
	{
		public PMS2()
		{
			this.m_TestId = 217;
			this.m_TestName = "PMS2";
            this.m_TestAbbreviation = "PMS2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
