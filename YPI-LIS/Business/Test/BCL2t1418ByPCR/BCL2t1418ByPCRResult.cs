using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCL2t1418ByPCR
{
	public class BCL2t1418ByPCRResult
    {
		protected string Method = "PCR/Fragment analysis method is used to detect the BCL2 translocation which involves the BCL2 " +
            "proto-oncogene on chromosome 18 and the immunoglobulin(Ig) heavy chain region on chromosome 14.  The primer designs " +
            "are based on BIOMED-2 publications.  Three sets of multiplexed primers encompass MBR region, 3’ MBR sub-cluster and mcr " +
            "region are amplified by PCR reactions.  The positivity of BCL2 t(14;18) translocation is evaluated based on the peak " +
            "size and peak height of the PCR product in each reaction well after capillary electrophoresis.  The ratio (%) is " +
            "calculated as (target peak height/ internal Ctrl peak height) *100%.  This assay has a sensitivity of detecting one " +
            "t(14;18) cell in approximately 400-1000 normal cells background.  Various factors including quantity and quality of " +
            "nucleic acid, sample preparation and sample age can affect assay performance.";
		protected string References = "1. Leich E, et al. Pathology, pathogenesis and molecular genetics of follicular NHL. " +
            "Best Pract Res Clin Haematol. 2011;24(2):95-109." + Environment.NewLine +
            "2. Piccaluga PP, et al.Biology and treatment of follicular lymphoma.Expert Rev Hematol. 2009;2(5):533-47.";
		protected string TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories.  " +
            "This test has not been approved by the FDA. The FDA has determined such clearance or approval is not necessary.  This " +
            "laboratory is CLIA certified to perform high complexity clinical testing.";


        public BCL2t1418ByPCRResult()
		{
		}

		public void SetResults(BCL2t1418ByPCRTestOrder panelSetOrder)
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
