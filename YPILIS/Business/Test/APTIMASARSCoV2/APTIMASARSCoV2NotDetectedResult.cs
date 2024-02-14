using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	public class APTIMASARSCoV2NotDetectedResult : APTIMASARSCoV2Result
	{
		public APTIMASARSCoV2NotDetectedResult()
		{            
            this.m_ResultCode = "SRSCV2NTDTCTD";
            this.m_Result = APTIMASARSCoV2Result.NOTDETECTED;            
		}
	}
}
