using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public EGFRMutationAnalysisPanel()
        {
            this.m_PanelId = 68;
            this.m_PanelName = "EGFR Mutation Analysis";
            this.m_AcknowledgeOnOrder = true;

            YellowstonePathology.Business.Test.Model.HandE handE = new YellowstonePathology.Business.Test.Model.HandE();
            this.m_TestCollection.Add(handE);            
        }
    }
}
