using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
    public class APTIMASARSCoV2DetectedResult : APTIMASARSCoV2Result
	{
		public APTIMASARSCoV2DetectedResult()
		{
            this.m_ResultCode = "SRSCV2DTCTD";
            this.m_Result = APTIMASARSCoV2Result.DETECTED;                        
        }
	}
}
