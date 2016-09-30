using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CongoRed : CytochemicalTest
	{
		public CongoRed()
		{
			this.m_TestId = 84;
			this.m_TestName = "Congo Red";
            this.m_TestAbbreviation = "Congo Red";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
