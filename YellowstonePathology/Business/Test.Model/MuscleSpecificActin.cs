using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class MuscleSpecificActin : ImmunoHistochemistryTest
	{
		public MuscleSpecificActin()
		{
			this.m_TestId = 125;
			this.m_TestName = "Muscle Specific Actin";
            this.m_TestAbbreviation = "MSA";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
