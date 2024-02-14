using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapQCPanel : ThinPrepPapScreeningPanel
    {
        public ThinPrepPapQCPanel()
        {
            this.m_IsQC = true;
            this.m_ScreeningType = "Cytotech Review";
        }       
    }
}
