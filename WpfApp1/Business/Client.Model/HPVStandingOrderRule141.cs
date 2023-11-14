using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule141 : StandingOrder
    {        
        public HPVStandingOrderRule141()
        {
            this.m_StandingOrderCode = "STNDHPVRL141";            
            this.m_ReflexOrder = new HPVReflexOrderRule141();
            this.m_Description = this.m_ReflexOrder.Description;            
        }        
    }
}
