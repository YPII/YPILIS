using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ERV3CT  : Test
	{
        public ERV3CT()
		{
			this.m_TestId = 33;
            this.m_TestName = "ERV3 CT";
            this.m_TestAbbreviation = "ERV3 CT";
			this.m_Active = true;
            this.m_NeedsAcknowledgement = false;
		}
	}
}
