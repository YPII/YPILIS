using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPL
{
	public class MPLResultNotDetected : TestResult
	{
        public MPLResultNotDetected()
		{
            this.m_ResultCode = "MPLNTDTCTD";
			this.m_Result = "Not Detected";			
		}
	}
}
