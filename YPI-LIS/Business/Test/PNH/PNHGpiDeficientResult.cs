using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHGpiDeficientResult : PNHResult
	{
		public PNHGpiDeficientResult()
		{
			this.m_Result = "GPI deficient cells identified";
			this.m_ResultCode = "PNHGPIDFCNT";
			this.m_Comment = "A very small population of GPI-deficient cells was identified.  The clinical significance of this finding is uncertain.  " +
				"Repeat testing in 3-6 months is recommended.";
		}
	}
}
