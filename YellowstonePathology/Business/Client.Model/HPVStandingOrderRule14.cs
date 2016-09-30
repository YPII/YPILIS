using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule14 : StandingOrder
    {        
        public HPVStandingOrderRule14()
        {
            this.m_StandingOrderCode = "STNDHPVRL14";            
            this.m_ReflexOrder = new HPVReflexOrderRule14();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
