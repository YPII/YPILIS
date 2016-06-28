using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Reticulin : CytochemicalTest
	{
		public Reticulin()
		{
			this.m_TestId = 151;
			this.m_TestName = "Reticulin";
            this.m_TestAbbreviation = "Reticulin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
