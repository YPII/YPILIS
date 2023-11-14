using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FactorVLeiden
{
	public class FactorVLeidenResult :YellowstonePathology.Business.Test.TestResult
	{
		protected string m_ResultDescription;
		protected string m_Interpretation;
        protected string m_Method = "This test was perforemed by an FDA approved Molecular PCR Assay.";
		protected string m_References = "Spector EB et al. American College of Medical Genetics: Technical Standards and Guidelines: Venous Thromboembolism " +
			"(Factor V Leiden and Prothrombin 20210G>A Testing): A Disease-Specific Supplement to the Standards and Guidelines for Clinical Genetics Laboratories. 2006.";
		protected string m_TestDevelopment = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It " +
			"has not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  " +
			"This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the " +
			"Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";

		public FactorVLeidenResult()
		{
		}

		public void SetResults(FactorVLeidenTestOrder testOrder)
		{
			testOrder.ResultDescription = this.m_ResultDescription;
			testOrder.Interpretation = this.m_Interpretation;
			testOrder.Method = this.m_Method;
			testOrder.ReportReferences = this.m_References;
			testOrder.TestDevelopment = this.m_TestDevelopment;
		}
	}
}
