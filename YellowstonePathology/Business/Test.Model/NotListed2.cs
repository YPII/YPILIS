using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NotListed2 : ImmunoHistochemistryTest
	{
		public NotListed2()
		{
			this.m_TestId = 128;
			this.m_TestName = "Not Listed #2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
