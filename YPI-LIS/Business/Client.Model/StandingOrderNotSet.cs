using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderNotSet : StandingOrder
    {
        public StandingOrderNotSet()
        {
            this.m_StandingOrderCode = "STNDNOTSET";
            this.m_Description = "Standing order is not set.";
            this.m_ReflexOrder = new ReflexOrderNone();
        }        
    }
}
