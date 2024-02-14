using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class AliquotOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;        

        public AliquotOrderReturnEventArgs(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            this.m_AliquotOrder = aliquotOrder;            
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }        
    }
}
