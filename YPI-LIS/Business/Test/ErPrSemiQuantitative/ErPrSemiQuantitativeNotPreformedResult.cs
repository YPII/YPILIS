using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeNotPreformedResult : ErPrSemiQuantitativeResult
	{
		public ErPrSemiQuantitativeNotPreformedResult()
		{
			this.m_Result = "Not Performed";
			this.m_ResultCode = "RRSMQNTTTVNTPRFMD";
			this.m_ErResult = "Not Performed";
			this.m_PrResult = "Not Performed";
		}
	}
}
