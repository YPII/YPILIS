using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class GlobalBillingRule : DomainBillingRule
    {
        YellowstonePathology.Business.Billing.Model.CptCodeCollection m_AllCptCodes;

        public GlobalBillingRule()
        {
            this.m_AllCptCodes = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
        }

        public override void Run(Domain.CptBillingCode cptBillingCode)
        {
            cptBillingCode.BillTo = BillToEnum.Patient.ToString();
        }        
    }
}
