using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Trichrome : CytochemicalTest
	{
		public Trichrome()
		{
			this.m_TestId = 160;
			this.m_TestName = "Trichrome";
            this.m_TestAbbreviation = "Trichrome";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
