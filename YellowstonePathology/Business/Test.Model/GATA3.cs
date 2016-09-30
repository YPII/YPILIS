using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class GATA3 : ImmunoHistochemistryTest
	{
		public GATA3()
		{
			this.m_TestId = 346;
			this.m_TestName = "GATA-3";
            this.m_TestAbbreviation = "GATA-3";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
