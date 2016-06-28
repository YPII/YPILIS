using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class SmoothMuscleMyosin : ImmunoHistochemistryTest
	{
		public SmoothMuscleMyosin()
        {
            this.m_TestId = 154;
			this.m_TestName = "Smooth Muscle Myosin";
            this.m_TestAbbreviation = "Smooth Muscle Myosin";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
        }
	}
}
