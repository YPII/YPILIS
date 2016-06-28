using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CytomegaloVirus : ImmunoHistochemistryTest
	{
		public CytomegaloVirus()
        {
            this.m_TestId = 350;
			this.m_TestName = "Cytomegalovirus";
            this.m_TestAbbreviation = "Cytomegalovirus";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
