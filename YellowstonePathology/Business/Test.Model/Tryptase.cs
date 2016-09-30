using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Tryptase : ImmunoHistochemistryTest
	{
        public Tryptase()
        {
            this.m_TestId = 353;
            this.m_TestName = "Tryptase";
            this.m_TestAbbreviation = "Tryptase";
            this.m_Active = true;
            this.m_NeedsAcknowledgement = true;
        }
	}
}
