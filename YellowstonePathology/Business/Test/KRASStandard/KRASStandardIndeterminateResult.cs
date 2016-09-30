using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardIndeterminateResult : KRASStandardResult
	{
		public KRASStandardIndeterminateResult()
		{
            this.m_ResultCode = "KRSSTDNDTRMNT";
			this.m_Result = "Indeterminate";			
			//this.m_ResultDescription = "Indeterminate";
		}
	}
}
