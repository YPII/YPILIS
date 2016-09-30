using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Fascin : ImmunoHistochemistryTest
	{
		public Fascin()
		{
			this.m_TestId = 102;
			this.m_TestName = "Fascin";
            this.m_TestAbbreviation = "Fascin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
