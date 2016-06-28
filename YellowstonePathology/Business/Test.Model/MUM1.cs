using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MUM1 : ImmunoHistochemistryTest
	{
		public MUM1()
		{
			this.m_TestId = 124;
			this.m_TestName = "MUM-1";
            this.m_TestAbbreviation = "MUM-1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
