using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeErPosPrNegResult : ErPrSemiQuantitativeResult
	{
		public ErPrSemiQuantitativeErPosPrNegResult()
		{
			this.m_Result = "ER – positive and PR – negative";
			this.m_ResultCode = "RRSMQNTTTVRPSTVRNGTV";
			this.m_ErResult = "Positive";
			this.m_PrResult = "Negative";
			this.m_Interpretation = "The results of the ER immunohistochemical assay shows 1% or greater positivity in the nuclei of tumor cells. The " +
				"results of the PR immunohistochemical assay shows less than 1% positivity in the nuclei of tumor cells." + Environment.NewLine + Environment.NewLine +
				"External controls reacted appropriately.  Therefore the results are ER – positive and PR – negative.";
		}
	}
}
