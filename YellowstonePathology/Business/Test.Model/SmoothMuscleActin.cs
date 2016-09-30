using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class SmoothMuscleActin : ImmunoHistochemistryTest
	{
		public SmoothMuscleActin()
		{
			this.m_TestId = 153;
			this.m_TestName = "Smooth Muscle Actin";
            this.m_TestAbbreviation = "Smooth Muscle Actin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
