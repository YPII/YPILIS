using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule7 : StandingOrder
    {
        public HPVStandingOrderRule7()
        {
            this.m_StandingOrderCode = "STNDHPVRL7";            
            this.m_ReflexOrder = new HPVReflexOrderRule7();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
