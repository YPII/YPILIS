using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	public class ReviewForAdditionalTestingResult : YellowstonePathology.Business.Test.TestResult
	{
		protected string m_Comment;

		public ReviewForAdditionalTestingResult()
		{
		}

		public void SetResults(ReviewForAdditionalTestingTestOrder testOrder)
		{
			testOrder.Result = this.m_Result;
			testOrder.Comment = this.m_Comment;
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults(ReviewForAdditionalTestingTestOrder testOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			if (testOrder.Final == true)
			{
				result.Success = false;
				result.Message = "Results can not be set because the case is already final.";
			}
			return result;
		}
	}
}
