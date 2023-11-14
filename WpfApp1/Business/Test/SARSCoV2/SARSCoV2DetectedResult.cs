using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
    public class SARSCoV2DetectedResult : SARSCoV2Result
	{
		public SARSCoV2DetectedResult()
		{
            this.m_ResultCode = "SRSCV2DTCTD";
            this.m_Result = SARSCoV2Result.DETECTED;                        
        }
	}
}
