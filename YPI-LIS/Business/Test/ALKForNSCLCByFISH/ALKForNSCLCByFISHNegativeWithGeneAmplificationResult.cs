using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHNegativeWithGeneAmplificationResult : ALKForNSCLCByFISHNegativeResult
	{
        public ALKForNSCLCByFISHNegativeWithGeneAmplificationResult()
		{
            this.m_ResultCode = "ALKNSCLCFSHNGTVWGAMP";
            this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative";
            this.m_ResultAbbreviation = "Negative";

			this.m_Interpretation = "An ALK gene rearrangement was observed in 0% of the nuclei scored but is below the cut-off for this assay. However, an abnormal signal " +
                "pattern of > or = 3 fusions (> or = 3F) was seen in *THREEFPERCENTAGE* of " +
                "nuclei scored. This finding is at or above our cutoff value of 36% and suggests ALK gene amplification or polysomy of chromosome 2. The " +
                "clinical significance of ALK gene amplification or chromosome 2 polysomy in patients with non-small cell lung cancer (NSCLC) is uncertain. " +
                "Polysomy is not currently an indication for crizotinib therapy.";

            this.m_ProbeSetDetail = "ALK Lung: There is no evidence of an ALK gene rearrangement, however, an abnormal signal pattern of three or more fusion signals (> or " +
                "=3F) was observed.";            
		}

        public override void SetResult(ALKForNSCLCByFISHTestOrder testOrder)
		{
            base.SetResult(testOrder);
            testOrder.Interpretation = this.m_Interpretation.Replace("*THREEFPERCENTAGE*", testOrder.ThreeFPercentage);            
		}
	}
}
