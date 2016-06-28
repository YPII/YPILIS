using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class WT1 : ImmunoHistochemistryTest
	{
		public WT1()
		{
			this.m_TestId = 173;
			this.m_TestName = "WT-1";
            this.m_TestAbbreviation = "WT1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
