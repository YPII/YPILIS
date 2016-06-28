using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD5 : ImmunoHistochemistryTest
	{
		public CD5()
		{
			this.m_TestId = 75;
			this.m_TestName = "CD5";
            this.m_TestAbbreviation = "CD5";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
