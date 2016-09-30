using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PAX5 : ImmunoHistochemistryTest
	{
		public PAX5()
        {
            this.m_TestId = 141;
			this.m_TestName = "PAX-5";
            this.m_TestAbbreviation = "PAX-5";
            this.m_Active = true;
            this.m_NeedsAcknowledgement = true;
        }
	}
}
