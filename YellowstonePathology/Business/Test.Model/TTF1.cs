using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TTF1 : ImmunoHistochemistryTest
	{
		public TTF1()
        {
            this.m_TestId = 161;
			this.m_TestName = "TTF-1";
            this.m_TestAbbreviation = "TTF-1";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
