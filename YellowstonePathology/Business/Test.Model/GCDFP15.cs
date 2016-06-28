using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class GCDFP15 : ImmunoHistochemistryTest
	{
		public GCDFP15()
		{
			this.m_TestId = 103;
			this.m_TestName = "GCDFP-15";
            this.m_TestAbbreviation = "GCDFP-15";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
