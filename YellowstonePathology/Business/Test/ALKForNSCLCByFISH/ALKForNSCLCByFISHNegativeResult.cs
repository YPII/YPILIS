using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHNegativeResult : ALKForNSCLCByFISHResult
	{
        public ALKForNSCLCByFISHNegativeResult()
		{
            this.m_ResultCode = "ALKNSCLCFSHNGTV";
			this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative";
            this.m_ResultAbbreviation = "Negative";
			
			this.m_Interpretation = "Interphase FISH analysis was performed using the ALK Break Apart FISH Probe Kit. FISH probe signals were within the normal reference " +
                "range. ALK gene rearrangement observations were below the cutoff of 10% for this assay. This represents " +
                "a NORMAL result and suggests that ALK inhibitors are not indicated.";
			
			this.m_ProbeSetDetail = "ALK Lung: A normal FISH signal pattern of two fusions (2F) was observed.";
		}

        public override void SetResult(ALKForNSCLCByFISHTestOrder testOrder)
		{
            base.SetResult(testOrder);
            testOrder.Interpretation = this.m_Interpretation.Replace("*FUSIONS*", testOrder.Fusions);
            testOrder.Interpretation = testOrder.Interpretation.Replace("*NUCLEIPERCENT*", testOrder.NucleiPercent);
            testOrder.ProbeSetDetail = this.m_ProbeSetDetail.Replace("*FUSIONS*", testOrder.Fusions);
		}
	}
}
