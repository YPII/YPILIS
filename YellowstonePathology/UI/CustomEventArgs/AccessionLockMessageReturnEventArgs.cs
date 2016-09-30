using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AccessionLockMessageReturnEventArgs : System.EventArgs
    {
        private UI.AppMessaging.AccessionLockMessage m_Message;

        public AccessionLockMessageReturnEventArgs(UI.AppMessaging.AccessionLockMessage message)
        {
            this.m_Message = message;
        }

        public UI.AppMessaging.AccessionLockMessage Message
        {
            get { return this.m_Message; }
        }
    }
}
