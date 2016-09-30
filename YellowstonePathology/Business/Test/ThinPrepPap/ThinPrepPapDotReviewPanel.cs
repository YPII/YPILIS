using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapDotReviewPanel : ThinPrepPapScreeningPanel
    {
        public ThinPrepPapDotReviewPanel()
        {
            this.m_ScreeningType = "DotReview";
            this.m_ResultCode = "55000";
        }       
    }
}
