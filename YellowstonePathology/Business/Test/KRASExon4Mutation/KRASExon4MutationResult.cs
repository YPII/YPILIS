using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASExon4Mutation
{
	public class KRASExon4MutationResult
	{
		public const string m_References = "1. Riely et al. Lung Cancer in 'Never-Smokers': Beyond EGFR Mutations and EGFR-TK Inhibitors. Clinical Cancer " + 
			"Research. 2008; 14:5731-5734.\r\n" +
			"2. Amado et al. Wild-Type KRAS Is Required for Panitumumab Efficacy in Patients With Metastatic Colorectal Cancer J Clin Oncol. 2008; 26:1626-1634.\r\n" +
			"3. Allegra et al. American Society of Clinical Oncology provisional clinical opinion: testing for KRAS gene mutations in patients with metastatic " +
			"colorectal carcinoma to predict response to anti-epidermal growth factor receptor monoclonal antibody therapy. J Clin Oncol. 2009; 27:2091-6.\r\n" +
			"4. Linardou H, et al. Clinical Implications of KRAS Mutations in Lung Cancer Patients Treated with Tyrosine Kinase Inhibitors: An Important Role " +
			"for Mutations in Minor Clones. Lancet Oncol. 2008; 9:962-972.";
		protected const string m_TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has " +
			"not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high " +
			"complexity clinical testing.";

		public KRASExon4MutationResult()
		{

		}

		public void SetResults(KRASExon4MutationTestOrder testOrder)
		{
			testOrder.ReportReferences = m_References;

			StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
			disclaimer.Append(m_TestDevelopment);
			testOrder.ReportDisclaimer = disclaimer.ToString();
		}

	}
}
