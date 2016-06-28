using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class AmyloidPProtein : ImmunoHistochemistryTest
	{
		public AmyloidPProtein()
		{
			this.m_TestId = 54;
			this.m_TestName = "Amyloid P Protein";
            this.m_TestAbbreviation = "Amyloid P Protein";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
