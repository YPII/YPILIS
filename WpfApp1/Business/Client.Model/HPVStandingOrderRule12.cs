using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule12 : StandingOrder
    {
        public HPVStandingOrderRule12()
        {
            this.m_StandingOrderCode = "STNDHPVRL12";            
            this.m_ReflexOrder = new HPVReflexOrderRule12();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
