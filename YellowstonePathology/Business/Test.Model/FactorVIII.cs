using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class FactorVIII : ImmunoHistochemistryTest
	{
		public FactorVIII()
		{
			this.m_TestId = 100;
			this.m_TestName = "Factor VIII";
            this.m_TestAbbreviation = "Factor VIII";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
