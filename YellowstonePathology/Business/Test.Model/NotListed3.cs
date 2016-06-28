using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NotListed3 : ImmunoHistochemistryTest
	{
		public NotListed3()
		{
			this.m_TestId = 129;
			this.m_TestName = "Not Listed #3";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
