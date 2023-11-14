using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class ClientOrderCollectionReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;

        public ClientOrderCollectionReturnEventArgs(YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            this.m_ClientOrderCollection = clientOrderCollection;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
        {
            get { return this.m_ClientOrderCollection; }
        }
    }
}
