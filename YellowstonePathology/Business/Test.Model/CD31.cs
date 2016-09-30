using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD31 : ImmunoHistochemistryTest
	{
		public CD31()
		{
			this.m_TestId = 176;
			this.m_TestName = "CD31";
            this.m_TestAbbreviation = "CD31";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
