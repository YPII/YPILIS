using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AOAccessionLockMessageReturnEventArgs : System.EventArgs
    {
        private UI.AppMessaging.AccessionLockMessage m_Message;
        private Business.Test.AccessionOrder m_AccessionOrder;

        public AOAccessionLockMessageReturnEventArgs(Business.Test.AccessionOrder accessionOrder, UI.AppMessaging.AccessionLockMessage message)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_Message = message;
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public UI.AppMessaging.AccessionLockMessage Message
        {
            get { return this.m_Message; }
        }
    }
}
