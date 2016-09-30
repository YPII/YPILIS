using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Iron : CytochemicalTest
	{
		public Iron()
		{
			this.m_TestId = 115;
			this.m_TestName = "Iron";
            this.m_TestAbbreviation = "Iron";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
