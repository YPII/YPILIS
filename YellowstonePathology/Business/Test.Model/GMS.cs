using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class GMS : CytochemicalForMicroorganisms
	{
		public GMS()
		{
			this.m_TestId = 106;
			this.m_TestName = "GMS";
            this.m_TestAbbreviation = "GMS";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
