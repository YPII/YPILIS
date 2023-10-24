using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeErPosPrPosResult : ErPrSemiQuantitativeResult
	{
		public ErPrSemiQuantitativeErPosPrPosResult()
		{
			this.m_Result = "Positive";
			this.m_ResultCode = "RRSMQNTTTVRPSTVRPSTV";
			this.m_ErResult = "Positive";
			this.m_PrResult = "Positive";
			this.m_Interpretation = "The results of the ER/PR immunohistochemical assays show 1% or greater positivity in the nuclei of tumor cells.  " +
				"External controls reacted appropriately.  Therefore the results are positive.";
		}
	}
}
