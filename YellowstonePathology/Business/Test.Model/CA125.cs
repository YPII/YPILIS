using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CA125 : ImmunoHistochemistryTest
	{
		public CA125()
		{
			this.m_TestId = 58;
			this.m_TestName = "CA 125";
            this.m_TestAbbreviation = "CA 125";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
