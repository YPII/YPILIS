using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TestOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;

        public TestOrderReturnEventArgs(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {
            this.m_TestOrder = testOrder;
        }

        public YellowstonePathology.Business.Test.Model.TestOrder TestOrder
        {
            get { return this.m_TestOrder; }
        }
    }
}
