using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AccessionOrderWithTrackerReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public AccessionOrderWithTrackerReturnEventArgs(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }
    }
}
