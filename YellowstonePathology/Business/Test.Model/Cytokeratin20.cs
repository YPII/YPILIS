using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin20 : ImmunoHistochemistryTest
	{
		public Cytokeratin20()
		{
			this.m_TestId = 87;
			this.m_TestName = "Cytokeratin 20";
            this.m_TestAbbreviation = "Cytokeratin 20";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
