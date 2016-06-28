using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PASforFungus : CytochemicalForMicroorganisms
	{
		public PASforFungus()
		{
			this.m_TestId = 139;
			this.m_TestName = "PAS for fungus";
            this.m_TestAbbreviation = "PAS for fungus";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
