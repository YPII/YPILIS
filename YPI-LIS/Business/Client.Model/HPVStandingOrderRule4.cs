using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule4 : StandingOrder
    {
        public HPVStandingOrderRule4()
        {
            this.m_StandingOrderCode = "STNDHPVRL4";            
            this.m_ReflexOrder = new HPVReflexOrderRule4();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
