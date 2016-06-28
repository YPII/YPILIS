using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class IFoundAContainerResult
    {
        private bool m_OkToReceive;
        private string m_Message;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;

        public IFoundAContainerResult(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
        {
            this.m_ClientOrderDetail = clientOrderDetail;
        }

        public bool OkToReceive
        {
            get { return this.m_OkToReceive; }
            set { this.m_OkToReceive = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
        {
            get { return this.m_ClientOrderDetail;}
        }
    }
}
