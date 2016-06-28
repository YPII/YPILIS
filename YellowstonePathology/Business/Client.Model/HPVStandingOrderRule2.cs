using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule2 : StandingOrder
    {        
        public HPVStandingOrderRule2()
        {
            this.m_StandingOrderCode = "STNDHPVRL2";            
            this.m_ReflexOrder = new HPVReflexOrderRule2();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
