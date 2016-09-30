using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class BRAF : NoCptCodeTest
	{
		public BRAF()
		{
			this.m_TestId = 222;
			this.m_TestName = "BRAF";
            this.m_TestAbbreviation = "BRAF";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
