using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD99 : ImmunoHistochemistryTest
	{
		public CD99()
		{
			this.m_TestId = 177;
			this.m_TestName = "CD99";
            this.m_TestAbbreviation = "CD99";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
