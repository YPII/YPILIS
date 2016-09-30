using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class P16 : ImmunoHistochemistryTest
	{
		public P16()
		{
			this.m_TestId = 132;
			this.m_TestName = "P16";
            this.m_TestAbbreviation = "P16";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
