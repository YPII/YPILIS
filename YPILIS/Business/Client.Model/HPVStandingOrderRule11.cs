using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule11 : StandingOrder
    {        
        public HPVStandingOrderRule11()
        {
            this.m_StandingOrderCode = "STNDHPVRL11";            
            this.m_ReflexOrder = new HPVReflexOrderRule11();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
