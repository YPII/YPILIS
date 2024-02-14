using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule3 : StandingOrder
    {
        public HPVStandingOrderRule3()
        {
            this.m_StandingOrderCode = "STNDHPVRL3";            
            this.m_ReflexOrder = new HPVReflexOrderRule3();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
