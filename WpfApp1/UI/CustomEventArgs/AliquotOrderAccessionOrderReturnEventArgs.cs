using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AliquotOrderAccessionOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public AliquotOrderAccessionOrderReturnEventArgs(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrder = aliquotOrder;
            this.m_AccessionOrder = accessionOrder;
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }
    }
}
