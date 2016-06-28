using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD45 : ImmunoHistochemistryTest
	{
		public CD45()
		{
			this.m_TestId = 73;
			this.m_TestName = "CD45";
            this.m_TestAbbreviation = "CD45";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
