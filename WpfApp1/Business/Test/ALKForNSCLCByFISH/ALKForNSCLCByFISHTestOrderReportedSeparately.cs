using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHTestOrderReportedSeparately : ALKForNSCLCByFISHTestOrder
    {
		public ALKForNSCLCByFISHTestOrderReportedSeparately()
        {
            this.Result = "The ALK result is reported in a separate document.";
        }
    }
}
