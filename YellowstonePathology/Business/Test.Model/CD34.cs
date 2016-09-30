using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD34 : ImmunoHistochemistryTest
	{
		public CD34()
		{
			this.m_TestId = 70;
			this.m_TestName = "CD34";
            this.m_TestAbbreviation = "CD34";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
