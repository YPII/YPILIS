using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHInconclusiveResult : ROS1ByFISHResult
	{
        public ROS1ByFISHInconclusiveResult()
		{            
            this.m_ResultCode = "ROS1FISHNCNCLSV";
			this.m_Result = "Inconclusive";
            this.m_ResultDisplayText = "Inconclusive";
            this.m_ResultAbbreviation = "Inconclusive";
            this.m_Interpretation = null;
            this.m_ProbeSetDetail = null;
		}        
	}
}
