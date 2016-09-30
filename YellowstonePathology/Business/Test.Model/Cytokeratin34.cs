using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Cytokeratin34 : ImmunoHistochemistryTest
	{
		public Cytokeratin34()
        {
            this.m_TestId = 88;
			this.m_TestName = "Cytokeratin 34";
            this.m_TestAbbreviation = "Cytokeratin 34";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
