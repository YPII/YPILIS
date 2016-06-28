using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin818 : ImmunoHistochemistryTest
	{
		public Cytokeratin818()
		{
			this.m_TestId = 91;
			this.m_TestName = "Cytokeratin 8/18";
            this.m_TestAbbreviation = "Cytokeratin 8/18";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
