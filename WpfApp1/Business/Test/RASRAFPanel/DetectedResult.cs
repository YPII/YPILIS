using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class DetectedResult : RASRAFPanelResult
    {
        public DetectedResult()
        {            
            this.m_InterpretationFirstLine = "[DETECTEDMUTATIONLIST] mutations detected; No evidence of mutation in [NOTDETECTEDMUTATIONLIST].";
            this.m_Comment = "The presence of [DETECTEDMUTATIONLIST] mutations in colorectal cancer is associated with resistance to anti-EGFR therapy; however, patients may exhibit sensitivity " +
                "to MEK inhibitors. [DETECTEDMUTATIONLIST] mutations are also reported to be associated with shortened survival.";
        }

        public override void SetResults(RASRAFPanelTestOrder testOrder, ResultCollection resultCollection)
        {
            StringBuilder comment = new StringBuilder(this.m_Comment);
            comment = comment.Replace("[NOTDETECTEDMUTATIONLIST]", resultCollection.GetNotDetectedListString());
            comment = comment.Replace("[DETECTEDMUTATIONLIST]", resultCollection.GetDetectedListString());            

            testOrder.Comment = comment.ToString();
            base.SetResults(testOrder, resultCollection);
        }
    }
}
