using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHNotInterpretableResult : ALKForNSCLCByFISHResult
	{
        public ALKForNSCLCByFISHNotInterpretableResult()
		{
            this.m_ResultCode = "ALKNSCLCFSHNTINTRPRTBL";
			this.m_Result = "Not Interpretable";
            this.m_ResultDisplayText = "Not Interpretable";
            this.m_ResultAbbreviation = "Not Interpretable";
			
            this.m_Interpretation = null;
			
			this.m_ProbeSetDetail = "ALK Lung: A normal FISH signal pattern of two fusions (2F) was observed.";
		}        
	}
}