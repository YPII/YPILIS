using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	public class BCellClonalityByPCRResultCollection :YellowstonePathology.Business.Test.TestResultCollection
	{
		public BCellClonalityByPCRResultCollection()
		{
			this.Add(new BCellClonalityByPCRNegativeResult());
			this.Add(new BCellClonalityByPCRPositiveResult());
			this.Add(new BCellClonalityByPCRIndeterminateResult());
			this.Add(new BCellClonalityByPCRResult());
		}

		public BCellClonalityByPCRResult GetResultFromTests(BCellClonalityByPCRTestOrder testOrder)
		{
			BCellClonalityByPCRResult result = new BCellClonalityByPCRResult();
			if (testOrder.BCellFrameWork1.ToUpper().StartsWith("CLONAL") == true ||
				testOrder.BCellFrameWork2.ToUpper().StartsWith("CLONAL") == true ||
				testOrder.BCellFrameWork3.ToUpper().StartsWith("CLONAL") == true)
			{
				result = new BCellClonalityByPCRPositiveResult();
			}
			else if (testOrder.BCellFrameWork1.ToUpper().StartsWith("NOT CLONAL") == true ||
				testOrder.BCellFrameWork2.ToUpper().StartsWith("NOT CLONAL") == true ||
				testOrder.BCellFrameWork3.ToUpper().StartsWith("NOT CLONAL") == true)
			{
				result = new BCellClonalityByPCRNegativeResult();
			}
			else if (testOrder.BCellFrameWork1.ToUpper().StartsWith("INDETERMINATE") == true ||
				testOrder.BCellFrameWork2.ToUpper().StartsWith("INDETERMINATE") == true ||
				testOrder.BCellFrameWork3.ToUpper().StartsWith("INDETERMINATE") == true)
			{
				result = new BCellClonalityByPCRIndeterminateResult();
			}
			return result;
		}
	}
}
