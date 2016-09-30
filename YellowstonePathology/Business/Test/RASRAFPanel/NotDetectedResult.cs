using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class NotDetectedResult : RASRAFPanelResult
    {
        public NotDetectedResult()
        {            
            this.m_InterpretationFirstLine = "No evidence of mutation in [NOTDETECTEDMUTATIONLIST] genes.";
            this.m_Comment = "The lack of mutations in these genes in colorectal tumors suggests sensitivity to anti-EGFR antibodies.";
        }

        public override void SetResults(RASRAFPanelTestOrder testOrder, ResultCollection resultCollection)
        {            
            testOrder.Comment = this.m_Comment;
            base.SetResults(testOrder, resultCollection);
        }
    }
}
