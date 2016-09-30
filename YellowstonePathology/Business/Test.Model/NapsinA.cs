using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NapsinA : ImmunoHistochemistryTest
	{
		public NapsinA()
        {
            this.m_TestId = 270;
			this.m_TestName = "Napsin A";
            this.m_TestAbbreviation = "Napsin A";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
