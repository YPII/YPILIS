using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NotListed1 : ImmunoHistochemistryTest
	{
		public NotListed1()
		{
			this.m_TestId = 127;
			this.m_TestName = "Not Listed #1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
