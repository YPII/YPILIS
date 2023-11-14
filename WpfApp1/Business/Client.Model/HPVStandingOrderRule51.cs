using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule51 : StandingOrder
    {
        public HPVStandingOrderRule51()
        {
            this.m_StandingOrderCode = "STNDHPVRL51";            
            this.m_ReflexOrder = new HPVReflexOrderRule51();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
