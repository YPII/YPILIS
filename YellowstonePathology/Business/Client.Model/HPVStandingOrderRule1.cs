using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule1 : StandingOrder
    {        
        public HPVStandingOrderRule1()
        {
            this.m_StandingOrderCode = "STNDHPVRL1";
            this.m_ReflexOrder = new HPVReflexOrderRule1();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
