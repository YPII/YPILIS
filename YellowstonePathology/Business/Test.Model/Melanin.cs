using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Melanin : CytochemicalTest
	{
		public Melanin()
		{
			this.m_TestId = 120;
			this.m_TestName = "Melanin";
            this.m_TestAbbreviation = "Melanin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
