using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class ClientOrderDetailReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;

        public ClientOrderDetailReturnEventArgs(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
        {
            this.m_ClientOrderDetail = clientOrderDetail;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
        {
            get { return this.m_ClientOrderDetail; }
        }
    }
}
