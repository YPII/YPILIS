using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKIndeterminateResult : BRAFV600EKResult
	{		
		public BRAFV600EKIndeterminateResult()
		{
			this.m_ResultCode = "BRAFV600EKNDTRMNT";
            this.m_Result =  "Indeterminate";
		}
	}
}
