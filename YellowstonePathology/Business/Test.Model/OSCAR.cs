using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class OSCAR : ImmunoHistochemistryTest
	{
		public OSCAR()
        {
            this.m_TestId = 170;
			this.m_TestName = "OSCAR";
            this.m_TestAbbreviation = "OSCAR";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
