using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class Ki67 : ImmunoHistochemistryTest
    {
        public Ki67()
        {
            this.m_TestId = 349;
            this.m_TestName = "Ki-67";
            this.m_TestAbbreviation = "Ki-67";
            this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
    }
}
