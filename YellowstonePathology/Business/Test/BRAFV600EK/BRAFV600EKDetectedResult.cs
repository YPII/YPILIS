using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKDetectedResult : BRAFV600EKResult
	{		
		public BRAFV600EKDetectedResult()
		{
            this.m_ResultCode = "BRAFV600EKDTCTD";
            this.m_Result = "Detected";        		    	        
		}
	}
}
