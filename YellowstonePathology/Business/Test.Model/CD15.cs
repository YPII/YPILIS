using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD15 : ImmunoHistochemistryTest
	{
		public CD15()
		{
			this.m_TestId = 64;
			this.m_TestName = "CD15";
            this.m_TestAbbreviation = "CD15";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
