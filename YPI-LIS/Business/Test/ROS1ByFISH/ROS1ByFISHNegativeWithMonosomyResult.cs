using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHNegativeWithMonosomyResult : ROS1ByFISHResult
	{
        public ROS1ByFISHNegativeWithMonosomyResult()
		{            
            this.m_ResultCode = "ROS1FISHNGTVWMNSMY";
			this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative with Monosomy";
            this.m_ResultAbbreviation = "Negative";

            this.m_Interpretation = "Interphase FISH analysis was performed using a ROS1 Break Apart FISH Probe. A ROS1 gene rearrangement was " +
                "observed in 0% of the nuclei scored and is below the cut-off for this assay. However, an abnormal signal pattern of one fusion (1F) which is above " +
                "our cut-off value of 39% was seen and suggests a deletion or monosomy of chromosome 6. Since the possibility of sectioning artifact cannot be " +
                "excluded, the clinical significance of this result in patients with non-small cell lung cancer (NSCLC) is uncertain. Monosomy or deletion of the " +
                "ROS1 locus is not currently an indication for crizotinib therapy.";
            this.m_ProbeSetDetail = "ROS1: There is no evidence of an ROS1 gene rearrangement, however, an abnormal signal pattern of one fusion (1F) ";
        }        
	}
}
