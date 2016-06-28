using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Chromogranin : ImmunoHistochemistryTest
	{
		public Chromogranin()
		{
			this.m_TestId = 83;
			this.m_TestName = "Chromogranin";
            this.m_TestAbbreviation = "Chromogranin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
