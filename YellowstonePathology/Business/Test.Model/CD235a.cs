using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD235a : ImmunoHistochemistryTest
	{
		public CD235a()
		{
			this.m_TestId = 175;
			this.m_TestName = "CD235a (Glycophorin A)";
            this.m_TestAbbreviation = "CD235a";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
