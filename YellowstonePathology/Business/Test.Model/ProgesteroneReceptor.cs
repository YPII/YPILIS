using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ProgesteroneReceptor : ImmunoHistochemistryTest
	{
		public ProgesteroneReceptor()
		{
			this.m_TestId = 144;
			this.m_TestName = "Progesterone Receptor";
            this.m_TestAbbreviation = "PR";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
