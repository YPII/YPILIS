using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderNone : StandingOrder
    {
        public StandingOrderNone()
        {
            this.m_StandingOrderCode = "STNDNONE";
            this.m_Description = "No standing order";
            this.m_ReflexOrder = new ReflexOrderNone();
        }        
    }
}
