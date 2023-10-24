using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule10 : StandingOrder
    {        
        public HPVStandingOrderRule10()
        {
            this.m_StandingOrderCode = "STNDHPVRL10";            
            this.m_ReflexOrder = new HPVReflexOrderRule10();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
