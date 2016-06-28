using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD8 : ImmunoHistochemistryTest
	{
		public CD8()
		{
			this.m_TestId = 80;
			this.m_TestName = "CD8";
            this.m_TestAbbreviation = "CD8";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
