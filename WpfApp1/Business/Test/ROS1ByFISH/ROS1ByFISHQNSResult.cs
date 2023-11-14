using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHQNSResult : ROS1ByFISHResult
	{
        public ROS1ByFISHQNSResult()
		{            
            this.m_ResultCode = "ROS1FISHQNS";
			this.m_Result = "QNS";
            this.m_ResultDisplayText = "QNS";
            this.m_ResultAbbreviation = "QNS";
            this.m_Interpretation = null;
            this.m_ProbeSetDetail = null;
		}        
	}
}
