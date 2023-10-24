using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ErPrSemiQuantitative
{
	public class ErPrSemiQuantitativeResult : YellowstonePathology.Business.Test.TestResult
	{
		private string m_ReportReferences = "Hammond et al., ASCO/CAP Guideline Recommendations for testing ER/PR in breast cancer, Arch Path Lab Med 2010, 134:907-22.";
		private string m_Method = "The estrogen (ER) and progesterone (PR) immunohistochemical assays were performed on paraffin embedded tissue with the ER (SP1) and PR (SP42) rabbit monoclonal antibodies. The Ki-67 immunohistochemical assay was performed on paraffin-embedded tissue using Ventana Ki-67 clone 30-9 rabbit monoclonal antibody. The test was performed according to ASCO/CAP guidelines.";
        protected string m_FixationOutOfBoundsComment = "The specimen fixation time does not meet ASCO CAP guidelines (6 to 72 hours), which may cause false negative results.  Repeat testing on an alternate specimen that meets fixation time guidelines is recommended, if available.";

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

        public string ReportReferences
        {
            get { return this.m_ReportReferences; }
        }

        public string Method
        {
            get { return this.m_Method; }
        }

        public string Interpretation
        {
            get { return this.m_Interpretation; }
        }

        public void SetResults(ErPrSemiQuantitativeTestOrder panelSetOrderErPrSemiQuantitative, Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
			panelSetOrderErPrSemiQuantitative.ResultCode = this.m_ResultCode;
			panelSetOrderErPrSemiQuantitative.ReportReferences = this.m_ReportReferences;
			panelSetOrderErPrSemiQuantitative.Method = this.m_Method;			
			panelSetOrderErPrSemiQuantitative.Interpretation = this.m_Interpretation + $" Ki-67 stain highlights up to {panelSetOrderErPrSemiQuantitative.Ki67Result} of tumor cell nuclei.  External controls reacted appropriately.";

			if (specimenOrder.FixationDuration > 72 || specimenOrder.FixationDuration < 6)
            {
                specimenOrder.FixationComment = m_FixationOutOfBoundsComment;
            }
        }
	}
}
