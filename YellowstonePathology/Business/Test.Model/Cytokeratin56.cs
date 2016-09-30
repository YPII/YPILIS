using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin56 : ImmunoHistochemistryTest
	{
		public Cytokeratin56()
		{
			this.m_TestId = 89;
			this.m_TestName = "Cytokeratin 5/6";
            this.m_TestAbbreviation = "Cytokeratin 5/6";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
