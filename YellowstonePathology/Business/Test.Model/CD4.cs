using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD4 : ImmunoHistochemistryTest
	{
		public CD4()
		{
			this.m_TestId = 71;
			this.m_TestName = "CD4";
            this.m_TestAbbreviation = "CD4";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
