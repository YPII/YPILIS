using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PAX8 : ImmunoHistochemistryTest
	{
		public PAX8()
		{
			this.m_TestId = 253;
			this.m_TestName = "PAX-8";
            this.m_TestAbbreviation = "PAX-8";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
