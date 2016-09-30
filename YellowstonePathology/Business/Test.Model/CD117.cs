using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD117 : ImmunoHistochemistryTest
	{
		public CD117()
		{
			this.m_TestId = 62;
			this.m_TestName = "CD117";
            this.m_TestAbbreviation = "CD117";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
