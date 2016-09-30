using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Zap70 : ImmunoHistochemistryTest
	{
		public Zap70()
        {
            this.m_TestId = 165;
			this.m_TestName = "ZAP 70";
            this.m_TestAbbreviation = "ZAP 70";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
