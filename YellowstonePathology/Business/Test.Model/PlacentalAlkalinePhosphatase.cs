using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PlacentalAlkalinePhosphatase : ImmunoHistochemistryTest
	{
		public PlacentalAlkalinePhosphatase()
		{
			this.m_TestId = 142;
			this.m_TestName = "Placental Alkaline Phosphatase";
            this.m_TestAbbreviation = "Placental Alkaline Phosphatase";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
