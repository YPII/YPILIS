using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD138 : ImmunoHistochemistryTest
	{
		public CD138()
		{
			this.m_TestId = 63;
			this.m_TestName = "CD138";
            this.m_TestAbbreviation = "CD138";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
