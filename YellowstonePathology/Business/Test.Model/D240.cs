using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class D240 : ImmunoHistochemistryTest
	{
		public D240()
		{
			this.m_TestId = 224;
			this.m_TestName = "D2-40";
            this.m_TestAbbreviation = "D2-40";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
