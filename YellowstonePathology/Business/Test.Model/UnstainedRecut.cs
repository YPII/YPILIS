using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class UnstainedRecut : NoCptCodeTest
	{
		public UnstainedRecut()
		{
			this.m_TestId = 182;
			this.m_TestName = "Unstained Recut";
            this.m_TestAbbreviation = "Unstained Recut";            
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
