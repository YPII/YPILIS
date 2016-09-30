using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class IgLambda : ImmunoHistochemistryTest
	{
		public IgLambda()
        {
            this.m_TestId = 114;
			this.m_TestName = "Ig Lambda";
            this.m_TestAbbreviation = "Ig Lambda";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
