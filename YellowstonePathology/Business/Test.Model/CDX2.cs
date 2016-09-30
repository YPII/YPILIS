using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CDX2 : ImmunoHistochemistryTest
	{
		public CDX2()
        {
            this.m_TestId = 81;
			this.m_TestName = "CDX-2";
            this.m_TestAbbreviation = "CDX-2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
