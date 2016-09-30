using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class S100 : ImmunoHistochemistryTest
	{
		public S100()
		{
			this.m_TestId = 152;
			this.m_TestName = "S100";
            this.m_TestAbbreviation = "S100";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
