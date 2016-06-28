using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule13 : StandingOrder
    {        
        public HPVStandingOrderRule13()
        {
            this.m_StandingOrderCode = "STNDHPVRL13";            
            this.m_ReflexOrder = new HPVReflexOrderRule13();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
