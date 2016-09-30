using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHNegativeWithPolysomyResult : ALKForNSCLCByFISHNegativeResult
	{
        public ALKForNSCLCByFISHNegativeWithPolysomyResult()
		{
            this.m_ResultCode = "ALKNSCLCFSHNGTVWPLSMY";
            this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative with Polysomy";
            this.m_ResultAbbreviation = "Negative";

			this.m_Interpretation = "Interphase FISH analysis was performed using the ALK Break Apart FISH Probe Kit. ALK gene rearrangement observations were below " +
                "the cutoff of 10% for this assay.  However, an abnormal signal pattern of > or = 3 fusions (> or = 3F) was seen. This is above the cutoff value of " +
                "36% and suggests ALK gene amplification or polysomy of chromosome 2. The clinical significance of ALK gene amplification or chromosome 2 polysomy in " +
                "patients with non-small cell lung cancer (NSCLC) is uncertain. Polysomy is not currently an indication for crizotinib therapy.";

            this.m_ProbeSetDetail = "ALK Lung: There is no evidence of an ALK gene rearrangement, however, an abnormal signal pattern of three or more fusion signals (> or " +
                "=3F) was observed.";            
		}        
	}
}
