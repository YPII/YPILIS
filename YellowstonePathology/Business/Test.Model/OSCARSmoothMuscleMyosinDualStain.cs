using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class OSCARSmoothMuscleMyosinDualStain : DualStain
	{
		public OSCARSmoothMuscleMyosinDualStain()
		{
            this.m_TestId = "OSCRSMM";
			this.m_TestName = "OSCAR/Smooth Muscle Myosin";
			this.m_FirstTest = new OSCAR();
			this.m_SecondTest = new SmoothMuscleMyosin();
		}
	}
}
