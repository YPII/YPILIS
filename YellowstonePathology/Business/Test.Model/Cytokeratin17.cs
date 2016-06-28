using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin17 : ImmunoHistochemistryTest
	{
		public Cytokeratin17()
		{
			this.m_TestId = 86;
			this.m_TestName = "Cytokeratin 17";
            this.m_TestAbbreviation = "Cytokeratin 17";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
