using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHPositiveResult : ROS1ByFISHResult
	{
        public ROS1ByFISHPositiveResult()
		{            
            this.m_ResultCode = "ROS1FISHPOSTV";
			this.m_Result = "Positive";
            this.m_ResultDisplayText = "Positive";
            this.m_ResultAbbreviation = "Positive";
			
			this.m_Interpretation = "Interphase FISH analysis was performed using a ROS1 Break Apart FISH Probe. A ROS1 gene rearrangement was observed in 0% of the " +
                "nuclei scored and is below the cut-off for this assay. However, an abnormal signal pattern of > or = 3 fusions (> or = 3F) was seen in 62% of " +
                "nuclei scored which is above our cut-off value of 22% and suggests ROS1 gene amplification or polysomy of chromosome 6. The clinical " +
                "significance of ROS1 gene amplification or chromosome 6 polysomy in patients with non-small cell lung cancer (NCSLC) is unknown at this " +
                "time. Polysomy or amplification of the ROS1 locus is not currently an indication for crizotinib therapy.";

            this.m_ProbeSetDetail = "ROS1: nuc ish(ROS1x3~>3)[xx/yy]";
		}        
	}
}
