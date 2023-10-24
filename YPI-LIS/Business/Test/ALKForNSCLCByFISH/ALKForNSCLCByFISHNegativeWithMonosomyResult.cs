using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHNegativeWithMonosomyResult : ALKForNSCLCByFISHNegativeResult
	{
        public ALKForNSCLCByFISHNegativeWithMonosomyResult()
		{
            this.m_ResultCode = "ALKNSCLCFSHNGTVWMNSMY";
            this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative with Monosomy";
            this.m_ResultAbbreviation = "Negative";

            this.m_Interpretation = "Interphase FISH analysis was performed using a ALK Break Apart FISH Probe. An ALK gene rearrangement was " +
               "observed in 0% of the nuclei scored and is below the cut-off for this assay. However, an abnormal signal pattern of one fusion (1F) which is above " +
               "our cut-off value of 39% was seen and suggests a deletion or monosomy of chromosome 6. Since the possibility of sectioning artifact cannot be " +
               "excluded, the clinical significance of this result in patients with non-small cell lung cancer (NSCLC) is uncertain. Monosomy or deletion of the " +
               "ALK locus is not currently an indication for crizotinib therapy.";

            this.m_ProbeSetDetail = "ALK: There is no evidence of an ALK gene rearrangement, however, an abnormal signal pattern of one fusion (1F) ";
        }        
	}
}
