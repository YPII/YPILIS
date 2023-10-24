using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
    public class HPV1618ByPCRPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public HPV1618ByPCRPanel()
        {
            this.m_PanelId = 65;
            this.m_PanelName = "HPV Genotypes 16 and 18";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.ParaffinCurls paraffinCurls = new Model.ParaffinCurls();
            this.m_TestCollection.Add(paraffinCurls);

            YellowstonePathology.Business.Test.Model.HandEAfterSlide handEAfterSlide = new Model.HandEAfterSlide();
            this.m_TestCollection.Add(handEAfterSlide);
        }
    }
}
