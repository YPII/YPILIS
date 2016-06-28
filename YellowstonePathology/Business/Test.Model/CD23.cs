using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD23 : ImmunoHistochemistryTest
	{
		public CD23()
		{
			this.m_TestId = 66;
			this.m_TestName = "CD23";
            this.m_TestAbbreviation = "CD23";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
