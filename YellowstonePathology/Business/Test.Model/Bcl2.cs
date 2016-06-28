using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Bcl2 : ImmunoHistochemistryTest
	{
		public Bcl2()
		{
			this.m_TestId = 56;
			this.m_TestName = "Bcl-2";
            this.m_TestAbbreviation = "Bcl-2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
