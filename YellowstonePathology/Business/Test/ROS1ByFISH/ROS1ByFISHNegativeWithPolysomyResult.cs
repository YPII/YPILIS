using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHNegativeWithPolysomyResult : ROS1ByFISHResult
	{
        public ROS1ByFISHNegativeWithPolysomyResult()
		{            
            this.m_ResultCode = "ROS1FISHNGTVWPLSMY";
			this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative with Polysomy";
            this.m_ResultAbbreviation = "Negative";

            this.m_Interpretation = "Interphase FISH analysis was performed using a ROS1 Break Apart FISH Probe Kit. ROS1 gene rearrangement observations were " +
                "below the cutoff of 10% for this assay.  However, an abnormal signal pattern of > or = 3 fusions (> or = 3F) was seen. This is above the cutoff " +
                "value of 22% and suggests ROS1 gene amplification or polysomy of chromosome 6. The clinical significance of ROS1 gene amplification or chromosome " +
                "6 polysomy in patients with non-small cell lung cancer (NSCLC) is uncertain. Polysomy is not currently an indication for crizotinib therapy.";

            this.m_ProbeSetDetail = "ROS1: There is no evidence of an ROS1 gene rearrangement, however, an abnormal signal pattern of three or more fusion signals (> or " +
                "=3F) was observed.";
        }        
	}
}
