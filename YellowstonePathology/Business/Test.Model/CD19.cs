using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD19 : ImmunoHistochemistryTest
	{
		public CD19()
		{
			this.m_TestId = 172;
			this.m_TestName = "CD19";
            this.m_TestAbbreviation = "CD19";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
