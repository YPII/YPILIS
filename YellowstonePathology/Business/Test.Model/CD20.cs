using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD20 : ImmunoHistochemistryTest
	{
		public CD20()
        {
            this.m_TestId = 65;
			this.m_TestName = "CD20";
            this.m_TestAbbreviation = "CD20";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
