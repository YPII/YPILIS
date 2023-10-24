using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule8 : StandingOrder
    {
        public HPVStandingOrderRule8()
        {
            this.m_StandingOrderCode = "STNDHPVRL8";            
            this.m_ReflexOrder = new HPVReflexOrderRule8();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
