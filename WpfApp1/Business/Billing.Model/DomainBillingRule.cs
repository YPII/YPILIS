using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class DomainBillingRule
    {
        public DomainBillingRule()
        {

        }

        public virtual void Run(Domain.CptBillingCode cptBillingCode)
        {
            throw new Exception("Not Implemented Here");
        }        
    }
}
