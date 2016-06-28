using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Alk : CytochemicalTest
	{
		public Alk()
		{
			this.m_TestId = 274;
			this.m_TestName = "Alk";
            this.m_TestAbbreviation = "Alk";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
