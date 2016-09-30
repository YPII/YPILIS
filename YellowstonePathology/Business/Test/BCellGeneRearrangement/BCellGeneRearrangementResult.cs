using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellGeneRearrangement
{
	public class BCellGeneRearrangementResult
	{
		private const string m_Method = "Patient DNA was isolated, purified and subjected to PCR amplification using oligonucleotide consensus primers specific for the IgH gene " +
			"Framework Regions 1, 2, 3 and joining regions.  In addition, Ig Kappa gene rearrangement analysis was performed using specific oligonucleotides recognizing the " +
			"FR3 and Jk regions.  PCR products were separated by capillary gel electrophoresis and detected by fluorescence.";
		private const string m_References = "1. vanDongen J, et.al. Design and standardization of PCR primers and protocols for detection of clonal immunoglobulin and " +
			"T-cell receptor gene recombinations in suspect lymphoproliferations: Report of the BIOMED-2 Concerted Action BMH4-CT98-3936. Leukemia. 2003; 17:2257-2317.";
		private const string m_Test = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has not been approved by the FDA.  " +
			"The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high complexity clinical testing.";

		public BCellGeneRearrangementResult()
		{
		}

		public void SetResults(BCellGeneRearrangementTestOrder testOrder)
		{
			testOrder.Method = m_Method;
			testOrder.ReportReferences = m_References;

			StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
			disclaimer.AppendLine();
			disclaimer.Append(m_Test);
			testOrder.ReportDisclaimer = disclaimer.ToString();
		}
	}
}
