using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Ecadherin : ImmunoHistochemistryTest
	{
		public Ecadherin()
		{
			this.m_TestId = 94;
			this.m_TestName = "E-cadherin";
            this.m_TestAbbreviation = "E-cadherin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
