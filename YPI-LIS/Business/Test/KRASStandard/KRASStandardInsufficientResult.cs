using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardInsufficientResult : KRASStandardResult
	{
		public KRASStandardInsufficientResult()
		{
            this.m_ResultCode = "KRSSTDINS";
			this.m_Result = "Insufficient DNA to perform analysis";
            //this.m_ResultDescription = "Insufficient DNA to perform analysis";			
		}
	}
}
