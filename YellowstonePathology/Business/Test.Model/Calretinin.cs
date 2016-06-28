using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Calretinin : ImmunoHistochemistryTest
	{
		public Calretinin()
		{
			this.m_TestId = 60;
			this.m_TestName = "Calretinin";
            this.m_TestAbbreviation = "Calretinin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
