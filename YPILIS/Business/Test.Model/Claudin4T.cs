using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Claudin4T :ImmunoHistochemistryTest
	{
		public Claudin4T()
		{
			this.m_TestId = "401";
			this.m_TestName = "Claudin 4 T";
            this.m_TestAbbreviation = "Claudin 4 T";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
