using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CopperRhodanine : CytochemicalTest
	{
		public CopperRhodanine()
		{
			this.m_TestId = 213;
			this.m_TestName = "Copper rhodanine";
            this.m_TestAbbreviation = "Copper rhodanine";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
