using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Villin : ImmunoHistochemistryTest
	{
		public Villin()
        {
            this.m_TestId = 163;
			this.m_TestName = "Villin";
            this.m_TestAbbreviation = "Villin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
