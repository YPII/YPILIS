using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HelicobacterPylori : ImmunoHistochemistryTest
	{
		public HelicobacterPylori()
		{
			this.m_TestId = 107;
			this.m_TestName = "Helicobacter pylori";
            this.m_TestAbbreviation = "Helicobacter pylori";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
