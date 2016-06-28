using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class NotificationServiceSendResult
    {
        private bool m_NotificationSent;
        private Exception m_Exception;        

        public NotificationServiceSendResult()
        {
            
        }

        public bool NotificationSent
        {
            get { return this.m_NotificationSent; }
            set { this.m_NotificationSent = value; }
        }

        public Exception Exception
        {
            get { return this.m_Exception; }
            set { this.m_Exception = value; }
        }
    }
}
