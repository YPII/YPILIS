using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD52 : ImmunoHistochemistryTest
	{
		public CD52()
		{
			this.m_TestId = 76;
			this.m_TestName = "CD52";
            this.m_TestAbbreviation = "CD52";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
