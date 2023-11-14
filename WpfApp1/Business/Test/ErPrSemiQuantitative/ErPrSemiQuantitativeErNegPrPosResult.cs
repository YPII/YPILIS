using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeErNegPrPosResult : ErPrSemiQuantitativeResult
	{
		public ErPrSemiQuantitativeErNegPrPosResult()
		{
			this.m_Result = "ER – negative and PR – positive";
			this.m_ResultCode = "RRSMQNTTTVRNGTVRPSTV";
			this.m_ErResult = "Negative";
			this.m_PrResult = "Positive";
			this.m_Interpretation = "The results of the ER immunohistochemical assay shows less than 1% positivity in the nuclei of tumor cells. The " +
				"results of the PR immunohistochemical assay show greater than 1% positivity in the nuclei of tumor cells." + Environment.NewLine + Environment.NewLine +
				"External controls reacted appropriately.  Therefore the results are ER – negative and PR – positive.";
		}
	}
}
