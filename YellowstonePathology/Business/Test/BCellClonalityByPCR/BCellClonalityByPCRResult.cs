using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	public class BCellClonalityByPCRResult : YellowstonePathology.Business.Test.TestResult
	{
		protected string m_Interpretation;
		protected string m_Method;
		protected string m_References;
		protected string m_ASRComment;

		public BCellClonalityByPCRResult()
		{
			this.m_Method = "From the submitted specimen, Enzymatic digestion with lysis of cells was performed then DNA was extracted using an automated " +
				"method.  The extracted DNA was amplified by multiplex PCR with 3 sets of primers targeting the framework regions of the Immunoglobulin heavy " +
				"chains (IgH).  Reaction products for all 3 tubes were fractionated using capillary electrophoresis and detected through differential fluorescence emission.";
			this.m_References = "1.  Miller JE, et al. An automated semiquantitative B and T cell clonality assay. Mol. Diag. 1999, 4(2): 101-117." + Environment.NewLine +
				"2.  Van Dongen, JJM et al. Design and standardization of PCR primers and protocols for the detection of clonal immunoglobulin and T-cell " +
				"receptor gene recombinations in suspect lymphoproliferations: Report of the BIOMED-2 Concerted Action BMH4-CT98-3936. Leukemia. 2003, " +
				"17(12): 2257-2317." + Environment.NewLine +
				"3.  van Krieken JHJM, Langerak AW, Macintyre EA, et.al. Improved reliability of lymphoma diagnostics via PCR-based clonality testing. " +
				"Report of the BIOMED-2 Concerted Action BHM4-CT98-3936. Leukemia 2007, 21:201-206.";
			this.m_ASRComment = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not " +
				"been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  " +
				"This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under " +
				"the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
		}

		public void SetResults(BCellClonalityByPCRTestOrder testOrder)
		{
			testOrder.Result = this.m_Result;
			testOrder.ResultCode = this.m_ResultCode;
			testOrder.Interpretation = this.m_Interpretation;
			testOrder.Method = this.m_Method;
			testOrder.References = this.m_References;
			testOrder.ASRComment = this.m_ASRComment;
		}
	}
}
