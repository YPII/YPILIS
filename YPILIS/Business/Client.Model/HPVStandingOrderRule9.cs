using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule9 : StandingOrder
    {
        public HPVStandingOrderRule9()
        {
            this.m_StandingOrderCode = "STNDHPVRL9";            
            this.m_ReflexOrder = new HPVReflexOrderRule9();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
