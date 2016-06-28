using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
    public class BCellClonalityByPCRPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public BCellClonalityByPCRPanel()
        {
            this.m_PanelId = 67;
            this.m_PanelName = "BCell Clonality By PCR";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.ParaffinCurls paraffinCurls = new Model.ParaffinCurls();
            this.m_TestCollection.Add(paraffinCurls);

            YellowstonePathology.Business.Test.Model.HandEAfterSlide handEAfterSlide = new Model.HandEAfterSlide();
            this.m_TestCollection.Add(handEAfterSlide);
        }
    }
}
