using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeResult : YellowstonePathology.Business.Test.TestResult
	{
		private string m_ReportReferences = "Hammond et al., ASCO/CAP Guideline Recommendations for testing ER/PR in breast cancer, Arch Path Lab Med 2010, 134:907-22.";
		private string m_Method = "The estrogen (ER) and progesterone (PR) immunohistochemical assays were performed on paraffin embedded tissue with the ER (SP1) and PR (SP42) rabbit monoclonal antibodies.  The test was performed according to ASCO/CAP guidelines.";

		protected string m_ErResult;
		protected string m_PrResult;
		protected string m_Interpretation;

		public ErPrSemiQuantitativeResult()
		{

		}

		public string ErResult
		{
			get { return this.m_ErResult; }
		}

		public string PrResult
		{
			get { return this.m_PrResult; }
		}

		public void SetResults(ErPrSemiQuantitativeTestOrder panelSetOrderErPrSemiQuantitative)
		{
			panelSetOrderErPrSemiQuantitative.ResultCode = this.m_ResultCode;
			panelSetOrderErPrSemiQuantitative.ReportReferences = this.m_ReportReferences;
			panelSetOrderErPrSemiQuantitative.Method = this.m_Method;
			panelSetOrderErPrSemiQuantitative.Interpretation = this.m_Interpretation;
		}
	}
}
