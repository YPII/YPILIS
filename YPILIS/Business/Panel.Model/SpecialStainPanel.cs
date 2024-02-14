using System;

namespace YellowstonePathology.Business.Panel.Model
{	
	public class SpecialStainPanel : Panel
    {
        public SpecialStainPanel()
        {
            this.m_PanelId = 19;
            this.m_PanelName = "Stain Orders";
            this.m_AcknowledgeOnOrder = false;
		}	
	}
}
