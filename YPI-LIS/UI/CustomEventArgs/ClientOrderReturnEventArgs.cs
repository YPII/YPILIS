using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class ClientOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public ClientOrderReturnEventArgs(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_ClientOrder = clientOrder;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }
    }
}
