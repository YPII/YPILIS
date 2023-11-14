using System;

namespace YellowstonePathology.Business.Panel.Model
{	
	public class InitialPanel : Panel
    {		
        public InitialPanel()
        {
            this.m_PanelId = 54;
            this.m_PanelName = "Initial Panel";
            this.m_AcknowledgeOnOrder = true;
		}	
	}
}
