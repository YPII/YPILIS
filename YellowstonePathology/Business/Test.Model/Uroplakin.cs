using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Uroplakin : ImmunoHistochemistryTest
	{
		public Uroplakin()
		{
			this.m_TestId = 169;
			this.m_TestName = "Uroplakin";
            this.m_TestAbbreviation = "Uroplakin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
