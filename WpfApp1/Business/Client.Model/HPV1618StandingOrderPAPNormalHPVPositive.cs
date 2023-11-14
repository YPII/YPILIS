using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPV1618StandingOrderPAPNormalHPVPositive : StandingOrder
    {
        public HPV1618StandingOrderPAPNormalHPVPositive()
        {
            this.m_StandingOrderCode = "STNDHPV1618PAPNRMLHPVPOS";
            this.m_Description = "Order HPV 16/18 when the PAP result is normal and the HPV result is positive";
            this.m_ReflexOrder = new HPV1618ReflexOrderPAPNormalHPVPositive();
        }        
    }
}
