using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Negative : NoCptCodeTest
	{
		public Negative()
		{
			this.m_TestId = "266";
			this.m_TestName = "Negative";
            this.m_TestAbbreviation = "Negative";
            this.m_Active = false;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
