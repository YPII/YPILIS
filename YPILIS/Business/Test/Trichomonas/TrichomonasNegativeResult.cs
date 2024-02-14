using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasNegativeResult : TrichomonasResult
    {
		public TrichomonasNegativeResult() 
        {
			this.m_ResultCode = "TRCHMNSNEG";
            this.m_Result = "Negative";
        }
    }
}
