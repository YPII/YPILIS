using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class ChlamydiaTrachomatis  : Test
	{
        public ChlamydiaTrachomatis()
		{
			this.m_TestId = 26;
            this.m_TestName = "Chlamydia trachomatis";
            this.m_TestAbbreviation = "Chlamydia trachomatis";
			this.m_Active = true;
            this.m_NeedsAcknowledgement = false;
		}
	}
}
