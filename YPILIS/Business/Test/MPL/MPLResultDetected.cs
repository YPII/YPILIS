using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPL
{
	public class MPLResultDetected : TestResult
	{
        public MPLResultDetected()
		{
            this.m_ResultCode = "MPLDTCTD";
			this.m_Result = "Detected";			
		}
	}
}
