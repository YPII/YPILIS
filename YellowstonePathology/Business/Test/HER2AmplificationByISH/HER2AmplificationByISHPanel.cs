using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationByISHPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public HER2AmplificationByISHPanel()
        {            
            this.m_PanelId = 56;
            this.m_PanelName = "HER2 Amplification by D-ISH";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.HandE handE = new Model.HandE();
            this.m_TestCollection.Add(handE);

            YellowstonePathology.Business.Test.Model.UnstainedSlide unstainedSlide1 = new Model.UnstainedSlide();            
            this.m_TestCollection.Add(unstainedSlide1);

            YellowstonePathology.Business.Test.Model.UnstainedSlide unstainedSlide2 = new Model.UnstainedSlide();            
            this.m_TestCollection.Add(unstainedSlide2);
        }
    }
}
