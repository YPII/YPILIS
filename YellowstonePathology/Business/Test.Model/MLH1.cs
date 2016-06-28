using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MLH1 : ImmunoHistochemistryTest
	{
		public MLH1()
		{
			this.m_TestId = 121;
			this.m_TestName = "MLH1";
            this.m_TestAbbreviation = "MLH1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
