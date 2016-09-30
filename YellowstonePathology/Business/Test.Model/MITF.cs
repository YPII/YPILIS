using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MITF : ImmunoHistochemistryTest
	{
		public MITF()
		{
			this.m_TestId = 178;
			this.m_TestName = "MITF";
            this.m_TestAbbreviation = "MITF";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
