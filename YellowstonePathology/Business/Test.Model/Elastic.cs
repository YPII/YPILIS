using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Elastic : CytochemicalTest
	{
		public Elastic()
		{
			this.m_TestId = 96;
			this.m_TestName = "Elastic";
            this.m_TestAbbreviation = "Elastic";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
