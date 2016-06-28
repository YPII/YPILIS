using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD30 : ImmunoHistochemistryTest
	{
		public CD30()
		{
			this.m_TestId = 69;
			this.m_TestName = "CD30";
            this.m_TestAbbreviation = "CD30";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
