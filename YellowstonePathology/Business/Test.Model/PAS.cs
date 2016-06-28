using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PAS : CytochemicalTest
	{
		public PAS()
		{
			this.m_TestId = 137;
			this.m_TestName = "PAS";
            this.m_TestAbbreviation = "PAS";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
