using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class CallingPageReturnEventArgs : System.EventArgs
    {
        Type m_CallingPage;

        public CallingPageReturnEventArgs(Type callingPage)
        {
            this.m_CallingPage = callingPage;
        }

        public Type CallingPage
        {
            get { return this.m_CallingPage; }
        }
    }
}
