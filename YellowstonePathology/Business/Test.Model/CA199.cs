using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CA199 : ImmunoHistochemistryTest
	{
		public CA199()
		{
			this.m_TestId = 59;
			this.m_TestName = "CA 19-9";
            this.m_TestAbbreviation = "CA 19-9";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
