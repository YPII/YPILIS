using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NPM1
{
	public class NPM1Result
	{
		public static string Method = "The NPM1 mutation is tested by fragment analysis method.  The NPM1 mutated or wildtype products are verified by determining the size of " +
			"PCR products as the wildtype displays a 210bp peak, while an NPM1 mutant displays an extra 214bp peak in addition to the wildtype peak.  " +
			"The mutation rate is calculated based on ratio of peak height [mutated/ (mutated+wildtype)] *100%.  This assay has a sensitivity of 5-10% for " +
			"detecting mutated NPM1 DNA in the wildtype background.  Various factors including quantity and quality of nucleic acid, sample preparation " +
			"and sample age can affect assay performance.";
		public static string References = "1. Falini B, et. al. Cytoplasmic nucleophosmin in acute myelogenous leukemia with a normal karyotype. NEJM. 2005; 352:254-66.";

		protected string m_Result;
		protected string m_PercentageNPM1Mutation;
		protected string m_Interpretation;

		public NPM1Result()
		{
		}

		public void SetResults(PanelSetOrderNPM1 panelSetOrderNPM1)
		{
			panelSetOrderNPM1.Result = this.m_Result;
			panelSetOrderNPM1.PercentageNPM1Mutation = this.m_PercentageNPM1Mutation;
			panelSetOrderNPM1.Interpretation = this.m_Interpretation;
			panelSetOrderNPM1.Method = NPM1Result.Method;
			panelSetOrderNPM1.References = NPM1Result.References;
		}
	}
}
