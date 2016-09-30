using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHSignificantPositiveResult : PNHResult
	{
		public PNHSignificantPositiveResult()
		{
			this.m_Result = "PNH clone identified.";
			this.m_ResultCode = "PNHSGNFCNTPSTV";
			this.m_Comment = "Flow cytometric analysis identified a PNH clonal population within the granulocytes (GRANULOCYTETOTAL%), " +
				"monocytes (MONOCYTETOTAL%), and RBC's (REDBLOODTOTAL%).  " +
				"Consider follow-up testing in 6 months to monitor PNH clone size.";
		}

		public override void SetResults(PNHTestOrder testOrder)
		{
			string result = this.m_Comment;
			result = result.Replace("GRANULOCYTETOTAL", this.GranulocytesTotal.ToString("F"));
			result = result.Replace("MONOCYTETOTAL", this.MonocytesTotal.ToString("F"));
			result = result.Replace("REDBLOODTOTAL", this.RedBloodTotal.ToString("F"));
			this.m_Comment = result;
			base.SetResults(testOrder);
		}
	}
}
