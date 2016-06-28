using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class NoHPVStandingOrder : StandingOrder
    {
        public NoHPVStandingOrder()
        {
            this.m_StandingOrderCode = "STNDHPVNONE";
            this.m_ReflexOrder = new ReflexOrderNone();
            this.m_Description = "No standing order.";
          
        }        
    }
}
