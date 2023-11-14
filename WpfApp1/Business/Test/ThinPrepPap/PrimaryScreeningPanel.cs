using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class PrimaryScreeningPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public PrimaryScreeningPanel()
        {
            this.m_PanelId = 38;
            this.m_PanelName = "Primary Screening";
            this.m_AcknowledgeOnOrder = true;
        }
    }
}
