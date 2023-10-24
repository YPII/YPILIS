using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FLT3
{
	public class FLT3Result
	{
		public static string Method = "The FLT3-ITD mutation was tested by fragment analysis.  The FLT3 wildtype and the ITD peak size and peak height are evaluated and used " +
			"for mutation rate calculation [ITD/ (ITD+wildtype)] *100%.  The FLT3-TKD somatic mutation was tested by Sanger sequencing method.  The " +
			"entire FLT3 exon 20 (C807_N847) is analyzed for all possible mutations and compared to database NM_004119 sequence.  This assay has a " +
			"sensitivity of 10-15% for detecting FLT3-TKD mutants and a sensitivity of 5-10% for detecting FLT3-ITD in wildtype background.  The " +
			"sensitivity is higher when plasma is used.  Various factors including quantity and quality of nucleic acid, sample preparation and sample age " +
			"can affect assay performance.";
		public static string References = "Mead AJ, et al. FLT3 tyrosine kinase domain mutations are biologically distinct from and have a significantly more favorable prognosis than " +
			"FLT3 internal tandem duplications in patients with acute myeloid leukemia. Blood. 2007; 110:1262-70.";

		protected string m_Result;
		protected string m_ITDMutation;
		protected string m_ITDPercentage;
		protected string m_TKDMutation;
		protected string m_Interpretation;

		public FLT3Result()
		{
		}

		public void SetResults(PanelSetOrderFLT3 panelSetOrderFLT3)
		{
			panelSetOrderFLT3.Result = this.m_Result;
			panelSetOrderFLT3.ITDMutation = this.m_ITDMutation;
			panelSetOrderFLT3.ITDPercentage = this.m_ITDPercentage;
			panelSetOrderFLT3.TKDMutation = this.m_TKDMutation;
			panelSetOrderFLT3.Interpretation = this.m_Interpretation;
			panelSetOrderFLT3.Method = FLT3Result.Method;
			panelSetOrderFLT3.ReportReferences = FLT3Result.References;
		}
	}
}
