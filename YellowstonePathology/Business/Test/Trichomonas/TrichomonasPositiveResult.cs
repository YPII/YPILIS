using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasPositiveResult : TrichomonasResult
    {
		public TrichomonasPositiveResult()
        {
			this.m_ResultCode = "TRCHMNSPOS";
			this.m_Result = "Positive";
        }
    }
}
