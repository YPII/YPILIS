using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ERV3  : Test
	{
        public ERV3()
		{
			this.m_TestId = 29;
            this.m_TestName = "ERV3";
            this.m_TestAbbreviation = "ERV3";
			this.m_Active = true;
            this.m_NeedsAcknowledgement = false;
		}
	}
}
