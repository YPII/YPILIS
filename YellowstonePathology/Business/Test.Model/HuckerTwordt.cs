using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HuckerTwordt : CytochemicalForMicroorganisms
	{
		public HuckerTwordt()
		{
			this.m_TestId = 112;
			this.m_TestName = "Hucker-Twordt";
            this.m_TestAbbreviation = "Hucker-Twordt";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
