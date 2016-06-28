using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HepatocyteSpecificAntigen : ImmunoHistochemistryTest
	{
		public HepatocyteSpecificAntigen()
		{
			this.m_TestId = 197;
			this.m_TestName = "Hepatocyte Specific Antigen";
            this.m_TestAbbreviation = "HSA";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
