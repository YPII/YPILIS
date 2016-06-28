using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class IgKappa : ImmunoHistochemistryTest
	{
		public IgKappa()
        {
            this.m_TestId = 113;
			this.m_TestName = "Ig Kappa";
            this.m_TestAbbreviation = "Ig Kappa";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
