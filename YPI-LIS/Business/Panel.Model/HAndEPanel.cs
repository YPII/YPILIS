using System;

namespace YellowstonePathology.Business.Panel.Model
{	
	public class HAndEPanel : Panel
    {		
        public HAndEPanel()
        {
            this.m_PanelId = 20;
            this.m_PanelName = "H and E Panel";
            this.m_AcknowledgeOnOrder = false;
		}	
	}
}
