using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Mucin : CytochemicalTest
	{
		public Mucin()
		{
			this.m_TestId = 123;
			this.m_TestName = "Mucin";
            this.m_TestAbbreviation = "Mucin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
