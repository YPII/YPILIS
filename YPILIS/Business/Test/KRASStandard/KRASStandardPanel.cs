using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
    public class KRASStandardPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public KRASStandardPanel()
        {
            this.m_PanelId = 36;
            this.m_PanelName = "KRAS";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.ParaffinCurls paraffinCurls = new Model.ParaffinCurls();
            this.m_TestCollection.Add(paraffinCurls);

            YellowstonePathology.Business.Test.Model.HandEAfterSlide handEAfterSlide = new Model.HandEAfterSlide();
            this.m_TestCollection.Add(handEAfterSlide);
        }
    }
}
