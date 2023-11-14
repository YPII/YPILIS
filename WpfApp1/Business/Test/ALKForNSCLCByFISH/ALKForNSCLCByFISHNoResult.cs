using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHNoResult : ALKForNSCLCByFISHResult
	{
        public ALKForNSCLCByFISHNoResult()
		{
            this.m_ResultCode = null;
            this.m_Result = null;
            this.m_ResultDisplayText = null;
            this.m_ResultAbbreviation = null;
            this.m_Interpretation = null;
            this.m_ProbeSetDetail = null;
            this.m_ReferenceRange = null;
            this.m_References = null;
            this.m_Method = null;
		}

        public override void SetResult(ALKForNSCLCByFISHTestOrder testOrder)
		{
            base.SetResult(testOrder);            
		}
	}
}
