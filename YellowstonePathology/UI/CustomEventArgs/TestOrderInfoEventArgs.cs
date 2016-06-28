using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TestOrderInfoEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.TestOrderInfo m_TestOrderInfo;

        public TestOrderInfoEventArgs(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
        {
            this.m_TestOrderInfo = testOrderInfo;
        }

        public YellowstonePathology.Business.Test.TestOrderInfo TestOrderInfo
        {
            get { return this.m_TestOrderInfo; }
            set { this.m_TestOrderInfo = value; }
        }
    }
}
