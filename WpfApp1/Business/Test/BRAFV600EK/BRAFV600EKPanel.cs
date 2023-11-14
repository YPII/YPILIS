using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
    public class BRAFV600EKPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public BRAFV600EKPanel()
        {
            this.m_PanelId = 24;
            this.m_PanelName = "BRAF";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.ParaffinCurls paraffinCurls = new Model.ParaffinCurls();
            this.m_TestCollection.Add(paraffinCurls);

            YellowstonePathology.Business.Test.Model.HandEAfterSlide handEAfterSlide = new Model.HandEAfterSlide();
            this.m_TestCollection.Add(handEAfterSlide);
        }
    }
}
