using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeErNegPrNegResult : ErPrSemiQuantitativeResult
	{
		public ErPrSemiQuantitativeErNegPrNegResult()
		{
			this.m_Result = "Negative";
			this.m_ResultCode = "RRSMQNTTTVRNGRNG";
			this.m_ErResult = "Negative";
			this.m_PrResult = "Negative";
			this.m_Interpretation = "The results of the ER/PR immunohistochemical assays show less than 1% positivity in the nuclei of tumor cells.  External " +
				"controls reacted appropriately.  Therefore the results are negative.";
		}
	}
}
