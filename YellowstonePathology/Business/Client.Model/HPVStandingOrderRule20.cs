using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderRule20 : StandingOrder
    {
        public HPVStandingOrderRule20()
        {
            this.m_StandingOrderCode = "STNDHPVRL20";
            this.m_ReflexOrder = new HPVReflexOrderRule20();
            this.m_Description = this.m_ReflexOrder.Description;
        }
    }
}
