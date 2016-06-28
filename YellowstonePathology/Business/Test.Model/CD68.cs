using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD68 : ImmunoHistochemistryTest
	{
		public CD68()
		{
			this.m_TestId = 78;
			this.m_TestName = "CD68";
            this.m_TestAbbreviation = "CD68";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
