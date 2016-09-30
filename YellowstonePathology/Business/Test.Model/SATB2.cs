using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class SATB2 : ImmunoHistochemistryTest
	{
        public SATB2()
        {
            this.m_TestId = 357;
			this.m_TestName = "SATB2";
            this.m_TestAbbreviation = "SATB2";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
