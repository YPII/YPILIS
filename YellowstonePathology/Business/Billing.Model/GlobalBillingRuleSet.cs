using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class GlobalBillingRuleSet : BillingRuleSet
    {
        public GlobalBillingRuleSet()
        {
            this.m_BillingRuleSetId = "GLBL";
            this.m_BillingRuleSetIdOld = "187E6457-F908-41AF-8232-D7754CC3E0CC";
            this.m_BillingRuleSetName = "Global Rule Set";

            CptCodeCollection allCptCodes = CptCodeCollection.GetAll();

            BillingRule billingRule9 = new BillingRule();
            billingRule9.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule9.Priority = 9;            
            billingRule9.PatientType = new RuleValueAny();
            billingRule9.PrimaryInsurance = new RuleValueAny();
            billingRule9.SecondaryInsurance = new RuleValueAny();
            billingRule9.PostDischarge = new RuleValueAny();
            billingRule9.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule9);
        }
    }
}
