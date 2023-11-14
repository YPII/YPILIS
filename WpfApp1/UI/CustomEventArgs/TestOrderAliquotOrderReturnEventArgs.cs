using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TestOrderAliquotOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;
        YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;

        public TestOrderAliquotOrderReturnEventArgs(YellowstonePathology.Business.Test.Model.TestOrder testOrder, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            this.m_TestOrder = testOrder;
            this.m_AliquotOrder = aliquotOrder;
        }

        public YellowstonePathology.Business.Test.Model.TestOrder TestOrder
        {
            get { return this.m_TestOrder; }
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }
    }
}
