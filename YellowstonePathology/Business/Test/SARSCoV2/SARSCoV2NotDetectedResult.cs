using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SARSCoV2
{
	public class SARSCoV2NotDetectedResult : SARSCoV2Result
	{
		public SARSCoV2NotDetectedResult()
		{            
            this.m_ResultCode = "SRSCV2NTDTCTD";
            this.m_Result = SARSCoV2Result.NOTDETECTED;            
		}
	}
}
