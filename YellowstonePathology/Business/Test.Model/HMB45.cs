using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HMB45 : ImmunoHistochemistryTest
	{
		public HMB45()
		{
			this.m_TestId = 111;
			this.m_TestName = "HMB-45";
            this.m_TestAbbreviation = "HMB-45";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
