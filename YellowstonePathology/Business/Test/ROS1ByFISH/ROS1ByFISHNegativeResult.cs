using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHNegativeResult : ROS1ByFISHResult
	{
        public ROS1ByFISHNegativeResult()
		{            
            this.m_ResultCode = "ROS1FISHNGTV";
			this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative";
            this.m_ResultAbbreviation = "Negative";

            this.m_Interpretation = "Interphase FISH analysis was performed using a ROS1 Break Apart FISH Probe. A ROS1 gene rearrangement was " +
                "observed in 0% of the nuclei scored and is below the cut-off for this assay. However, an abnormal signal pattern of one fusion (1F) which is above " +
                "our cut-off value of 39% was seen and suggests a deletion or monosomy of chromosome 6. Since the possibility of sectioning artifact cannot be " +
                "excluded, the clinical significance of this result in patients with non-small cell lung cancer (NSCLC) is uncertain. Monosomy or deletion of the " +
                "ROS1 locus is not currently an indication for crizotinib therapy.";

            this.m_ProbeSetDetail = "ROS1: A normal FISH signal pattern of two fusions (2F) was observed.";
		}        
	}
}
