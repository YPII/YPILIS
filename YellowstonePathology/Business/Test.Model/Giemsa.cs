using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Giemsa : CytochemicalForMicroorganisms
	{
		public Giemsa()
		{
			this.m_TestId = 105;
			this.m_TestName = "Giemsa";
            this.m_TestAbbreviation = "Giemsa";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
