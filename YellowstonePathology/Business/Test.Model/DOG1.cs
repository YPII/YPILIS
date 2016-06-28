using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class DOG1 : ImmunoHistochemistryTest
	{
		public DOG1()
		{
			this.m_TestId = 298;
			this.m_TestName = "DOG-1";
            this.m_TestAbbreviation = "DOG-1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
