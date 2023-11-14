using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule5 : StandingOrder
    {
        public HPVStandingOrderRule5()
        {
            this.m_StandingOrderCode = "STNDHPVRL5";            
            this.m_ReflexOrder = new HPVReflexOrderRule5();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
