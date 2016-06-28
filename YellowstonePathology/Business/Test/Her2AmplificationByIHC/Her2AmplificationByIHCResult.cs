using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class Her2AmplificationByIHCResult
	{
		private const string Method = "Formalin-fixed paraffin-embedded tissue sections were stained with a primary antibody and a multimer-technology based " +
			"system was used for detection.  Stains were scored by a pathologist using manual microscopy.";

		private const string TestDevelopmentComment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories.  This test has " +
			"not been approved by the FDA.  The FDA has determined such clearance or approval is not necessary.  This laboratory is CLIA certified to perform high " +
			"complexity clinical testing.";

		public Her2AmplificationByIHCResult()
		{
		}

		public void SetResults(PanelSetOrderHer2AmplificationByIHC panelSetOrder)
		{
			panelSetOrder.Method = Method;

			StringBuilder disclaimer = new StringBuilder();
            string locationComment = panelSetOrder.GetLocationPerformedComment();
			disclaimer.AppendLine(locationComment);
			disclaimer.AppendLine(TestDevelopmentComment);
			panelSetOrder.ReportDisclaimer = disclaimer.ToString();
		}
	}
}
