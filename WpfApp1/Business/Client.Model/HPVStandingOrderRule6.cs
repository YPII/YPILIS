using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule6 : StandingOrder
    {
        public HPVStandingOrderRule6()
        {
            this.m_StandingOrderCode = "STNDHPVRL6";            
            this.m_ReflexOrder = new HPVReflexOrderRule6();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
