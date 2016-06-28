using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HER2Neu : ImmunoHistochemistryTest
	{
		public HER2Neu()
		{
			this.m_TestId = 108;
			this.m_TestName = "HER-2/Neu";
            this.m_TestAbbreviation = "HER-2/Neu";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
