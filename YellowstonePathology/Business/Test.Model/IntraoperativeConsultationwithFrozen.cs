using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class IntraoperativeConsultationwithFrozen : NoCptCodeTest
	{
		public IntraoperativeConsultationwithFrozen()
		{
			this.m_TestId = 45;
			this.m_TestName = "Intraoperative Consultation with Frozen";
            this.m_TestAbbreviation = "IC/F";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = false;
		}
	}
}
