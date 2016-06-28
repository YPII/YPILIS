using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ERV3NG  : Test
	{
        public ERV3NG()
		{
			this.m_TestId = 32;
            this.m_TestName = "ERV3 NG";
            this.m_TestAbbreviation = "ERV3 NG";
			this.m_Active = true;
            this.m_NeedsAcknowledgement = false;
		}
	}
}
