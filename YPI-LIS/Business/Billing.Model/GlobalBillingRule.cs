using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class GlobalBillingRule : DomainBillingRule
    {
        public GlobalBillingRule()
        {
        }

        public override void Run(Domain.CptBillingCode cptBillingCode)
        {
            cptBillingCode.BillTo = BillToEnum.Patient.ToString();
        }        
    }
}
