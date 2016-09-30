using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class MelanA : ImmunoHistochemistryTest
    {
        public MelanA()
        {
            this.m_TestId = 119;
            this.m_TestName = "Melan A";
            this.m_TestAbbreviation = "Melan A";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
    }
}
