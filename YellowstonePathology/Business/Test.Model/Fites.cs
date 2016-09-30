using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Fites : CytochemicalTest
	{
		public Fites()
		{
			this.m_TestId = 269;
			this.m_TestName = "Fites";
            this.m_TestAbbreviation = "Fites";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
