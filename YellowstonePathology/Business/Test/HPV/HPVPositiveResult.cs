using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVPositiveResult : HPVResult
	{
		public HPVPositiveResult()
		{            
            this.m_ResultCode = HPVResult.OveralResultCodePositive;
            this.m_Result = HPVResult.PositiveResult; 
		}
	}
}
