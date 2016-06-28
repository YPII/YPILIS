using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin7 : ImmunoHistochemistryTest
	{
		public Cytokeratin7()
		{
			this.m_TestId = 90;
			this.m_TestName = "Cytokeratin 7";
            this.m_TestAbbreviation = "Cytokeratin 7";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
