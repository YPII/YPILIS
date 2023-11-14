using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public ErPrSemiQuantitativeResultCollection()
		{
			this.Add(new ErPrSemiQuantitativeErPosPrPosResult());
			this.Add(new ErPrSemiQuantitativeErNegPrNegResult());
			this.Add(new ErPrSemiQuantitativeErPosPrNegResult());
			this.Add(new ErPrSemiQuantitativeErNegPrPosResult());
			this.Add(new ErPrSemiQuantitativeNotPreformedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}

		public ErPrSemiQuantitativeResult GetResult(string erResult, string prResult)
		{
			ErPrSemiQuantitativeResult result = new ErPrSemiQuantitativeResult();

			if (erResult == "Not Performed" || prResult == "Not Performed") result = new ErPrSemiQuantitativeNotPreformedResult();
			else
			{
				foreach (ErPrSemiQuantitativeResult erPrSemiQuantitativeResult in this)
				{
					if (erPrSemiQuantitativeResult.ErResult == erResult &&
						erPrSemiQuantitativeResult.PrResult == prResult)
					{
						result = erPrSemiQuantitativeResult;
						break;
					}
				}
			}
			return result;
		}
	}
}
