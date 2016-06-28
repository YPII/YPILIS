using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD25 : ImmunoHistochemistryTest
	{
		public CD25()
		{
			this.m_TestId = 67;
			this.m_TestName = "CD25";
            this.m_TestAbbreviation = "CD25";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
