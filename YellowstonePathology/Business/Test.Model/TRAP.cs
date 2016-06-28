using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TRAP : ImmunoHistochemistryTest
	{
		public TRAP()
		{
			this.m_TestId = 159;
			this.m_TestName = "TRAP";
            this.m_TestAbbreviation = "TRAP";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
