using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPV1618StandingOrderHPVPositive : StandingOrder
    {
        public HPV1618StandingOrderHPVPositive()
        {
            this.m_StandingOrderCode = "STNDHPV1618HPVPOS";
            this.m_Description = "Order HPV 16/18 when the HPV result is positive";
            this.m_ReflexOrder = new HPV1618ReflexOrderHPVPositive();
        }        
    }
}
