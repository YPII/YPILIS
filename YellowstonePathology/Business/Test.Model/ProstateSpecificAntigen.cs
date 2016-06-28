using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ProstateSpecificAntigen : ImmunoHistochemistryTest
	{
		public ProstateSpecificAntigen()
		{
			this.m_TestId = 146;
			this.m_TestName = "Prostate Specific Antigen";
            this.m_TestAbbreviation = "Prostate Specific Antigen";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
