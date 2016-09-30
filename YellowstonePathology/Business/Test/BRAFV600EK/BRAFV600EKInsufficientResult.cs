using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKInsufficientResult : BRAFV600EKResult
	{		
		public BRAFV600EKInsufficientResult()
		{
			this.m_ResultCode = "BRAFV600EKINS";
            this.m_Result = "Insufficient DNA to perform analysis";
		}
	}
}
