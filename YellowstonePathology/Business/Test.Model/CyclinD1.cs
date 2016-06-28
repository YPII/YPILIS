using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CyclinD1 : ImmunoHistochemistryTest
	{
		public CyclinD1()
		{
			this.m_TestId = 85;
			this.m_TestName = "Cyclin D1";
            this.m_TestAbbreviation = "Cyclin D1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
