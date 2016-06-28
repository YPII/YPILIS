using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NKX31 : ImmunoHistochemistryTest
	{
		public NKX31()
		{
			this.m_TestId = 355;
			this.m_TestName = "NKX3.1";
            this.m_TestAbbreviation = "NKX3.1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
