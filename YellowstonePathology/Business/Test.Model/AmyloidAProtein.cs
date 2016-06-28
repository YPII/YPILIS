using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class AmyloidAProtein : ImmunoHistochemistryTest
	{
		public AmyloidAProtein()
		{
			this.m_TestId = 53;
			this.m_TestName = "Amyloid A Protein";
            this.m_TestAbbreviation = "Amyloid A Protein";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
