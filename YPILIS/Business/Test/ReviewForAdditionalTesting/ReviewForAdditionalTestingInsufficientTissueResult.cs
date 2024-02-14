using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	public class ReviewForAdditionalTestingInsufficientTissueResult : ReviewForAdditionalTestingResult
	{
		public ReviewForAdditionalTestingInsufficientTissueResult()
		{
			this.m_Comment = "Insufficient tissue to perform test.";
			this.m_Result = "Insufficient tissue to perform test.";
		}
	}
}
