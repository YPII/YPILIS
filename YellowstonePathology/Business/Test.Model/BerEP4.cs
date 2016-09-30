using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class BerEP4 : ImmunoHistochemistryTest
	{
		public BerEP4()
		{
			this.m_TestId = 216;
			this.m_TestName = "Ber-EP4";
            this.m_TestAbbreviation = "Ber-EP4";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
