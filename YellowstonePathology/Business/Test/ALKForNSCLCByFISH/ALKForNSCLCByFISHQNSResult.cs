using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHQNSResult : ALKForNSCLCByFISHResult
	{
        public ALKForNSCLCByFISHQNSResult()
		{
            this.m_ResultCode = "ALKNSCLCFSHQNS";
			this.m_Result = "QNS";
            this.m_ResultDisplayText = "QNS";
            this.m_ResultAbbreviation = "QNS";
			
            this.m_Interpretation = null;					
		}        
	}
}