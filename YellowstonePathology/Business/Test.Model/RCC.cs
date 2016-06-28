using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class RCC : ImmunoHistochemistryTest
	{
		public RCC()
		{
			this.m_TestId = 149;
			this.m_TestName = "RCC";
            this.m_TestAbbreviation = "RCC";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
