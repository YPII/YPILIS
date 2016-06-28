using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKNotDetectedResult : BRAFV600EKResult
	{		
		public BRAFV600EKNotDetectedResult()
		{
            this.m_ResultCode = "BRAFV600EKNTDTCTD";
	        this.m_Result = "Not Detected";		    
		}
	}
}
