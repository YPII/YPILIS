using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHPersistentPositiveResult : PNHResult
	{
		public static string PNHPersistentPositiveResultResultCode = "PNHPSTVPRVSPSTV";

		public PNHPersistentPositiveResult()
		{
			this.m_Result = "Persistent PNH clone identified";
			this.m_ResultCode = PNHPersistentPositiveResult.PNHPersistentPositiveResultResultCode;
			this.m_Comment = "Flow cytometric analysis identified a persistent PNH clonal population.  Please see result data and result " +
				"monitoring sections below for comparison to previous results.";
		}
	}
}
