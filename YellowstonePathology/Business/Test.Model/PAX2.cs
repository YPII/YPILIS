using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PAX2 : ImmunoHistochemistryTest
	{
		public PAX2()
		{
			this.m_TestId = 174;
			this.m_TestName = "PAX-2";
            this.m_TestAbbreviation = "PAX-2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
