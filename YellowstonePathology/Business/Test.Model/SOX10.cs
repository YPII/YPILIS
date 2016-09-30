using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class SOX10 : ImmunoHistochemistryTest
	{
        public SOX10()
		{
			this.m_TestId = 356;
			this.m_TestName = "SOX10";
            this.m_TestAbbreviation = "SOX10";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
