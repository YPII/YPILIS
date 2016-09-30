using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class IntraoperativeConsultation : NoCptCodeTest
	{
		public IntraoperativeConsultation()
		{
			this.m_TestId = 194;
			this.m_TestName = "Intraoperative Consultation";
            this.m_TestAbbreviation = "IC";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
