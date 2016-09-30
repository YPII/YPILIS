using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TdT : ImmunoHistochemistryTest
	{
		public TdT()
		{
			this.m_TestId = 157;
			this.m_TestName = "TdT";
            this.m_TestAbbreviation = "TdT";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
