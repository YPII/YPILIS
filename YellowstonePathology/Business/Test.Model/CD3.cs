using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD3 : ImmunoHistochemistryTest
	{
		public CD3()
        {
            this.m_TestId = 68;
			this.m_TestName = "CD3";
            this.m_TestAbbreviation = "CD3";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
