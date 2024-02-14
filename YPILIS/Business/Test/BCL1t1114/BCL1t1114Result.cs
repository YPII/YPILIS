using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCL1t1114
{
	public class BCL1t1114Result
	{
		protected string Method = "This assay is a real-time quantitative PCR assay.  Extracted sample DNA is subjected to two tubes real-time PCR reaction to " +
			"measure the quantity of the t(11;14) fused gene and the internal control gene.  The result is reported as a ratio between the quantities of the " +
			"CCND1/IgH fused gene to the internal control gene.  The positivity (%) of t(11;14) is calculated as: (CCND1 pg)/ (Internal CTRL pg) *100%. This " +
			"assay has a sensitivity of detecting one t(11;14) cell in a background of approximately 1000 normal cells.  Various factors including quantity and " +
			"quality of nucleic acid, sample preparation and sample age can affect assay performance.";
		protected const string References = "1. R Luthra, AH Sarris, S Hai, AV Paladugu, JE Romaguera, FF Cabanillas, and LJ Medeiros. Real-Time 5' - 3' " +
			"exonuclease-based PCR assay for detection of the t(11;14) (q13;q32). Am J Clin Pathol 1999;112: 524-530.";
		protected string TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics " +
			"Laboratories.  This test has not been approved by the FDA.  The FDA has determined such clearance or approval is not necessary. This laboratory is " +
			"CLIA certified to perform high complexity clinical testing.";
		
		public BCL1t1114Result()
		{
		}

		public void SetResults(BCL1t1114TestOrder panelSetOrder)
		{
			panelSetOrder.Method = Method;
			panelSetOrder.ReportReferences = References;

			StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(panelSetOrder.GetLocationPerformedComment() + Environment.NewLine);
			disclaimer.AppendLine(TestDevelopment);
			panelSetOrder.ReportDisclaimer = disclaimer.ToString();
		}
	}
}
