using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PanelSetReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;

        public PanelSetReturnEventArgs(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            this.m_PanelSet = panelSet;
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
        }
    }
}
