using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PanelSetOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public PanelSetOrderReturnEventArgs(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }
    }
}
