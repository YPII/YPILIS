using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class HospitalGlobalBillingRuleSet : BillingRuleSet
    {
        public HospitalGlobalBillingRuleSet()
        {
            this.m_BillingRuleSetId = "HGLBL";
            this.m_BillingRuleSetIdOld = "20FC3546-B829-4F35-9571-D6EFE7A9954C";
            this.m_BillingRuleSetName = "Hospital Global Rule Set";

            CptCodeCollection allCptCodes = CptCodeCollection.GetAll();

            BillingRule billingRule0 = new BillingRule();
            billingRule0.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule0.Priority = 0;            
            billingRule0.PatientType = new RuleValueAny();
            billingRule0.PrimaryInsurance = new RuleValueAny();
            billingRule0.SecondaryInsurance = new RuleValueAny();
            billingRule0.PostDischarge = new RuleValueAny();
            billingRule0.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule0);

            BillingRule billingRule1 = new BillingRule();
            billingRule1.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule1.Priority = 1;            
            billingRule1.PatientType = new RuleValueAny();
            billingRule1.PrimaryInsurance = new RuleValueString("Medicare");
            billingRule1.SecondaryInsurance = new RuleValueAny();
            billingRule1.PostDischarge = new RuleValueAny();
            billingRule1.BillingType = BillingTypeEnum.Split;
            this.m_BillingRuleCollection.Add(billingRule1);

            BillingRule billingRule2 = new BillingRule();
            billingRule2.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule2.Priority = 2;            
            billingRule2.PatientType = new RuleValueAny();
            billingRule2.PrimaryInsurance = new RuleValueString("Governmental");
            billingRule2.SecondaryInsurance = new RuleValueAny();
            billingRule2.PostDischarge = new RuleValueAny();
            billingRule2.BillingType = BillingTypeEnum.Split;
            this.m_BillingRuleCollection.Add(billingRule2);

            BillingRule billingRule3 = new BillingRule();
            billingRule3.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule3.Priority = 3;
            billingRule3.PatientType = new RuleValueString("IP");
            billingRule3.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule3.SecondaryInsurance = new RuleValueAny();            
            billingRule3.PostDischarge = new RuleValueAny();
            billingRule3.BillingType = BillingTypeEnum.Split;
            this.m_BillingRuleCollection.Add(billingRule3);

            BillingRule billingRule4 = new BillingRule();
            billingRule4.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule4.Priority = 4;
            billingRule4.PatientType = new RuleValueString("IP");
            billingRule4.PrimaryInsurance = new RuleValueAny();
            billingRule4.SecondaryInsurance = new RuleValueString("Medicaid");
            billingRule4.PostDischarge = new RuleValueAny();
            billingRule4.BillingType = BillingTypeEnum.Split;
            this.m_BillingRuleCollection.Add(billingRule4);            

            BillingRule billingRule9 = new BillingRule();
            billingRule9.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule9.Priority = 9;            
            billingRule9.PatientType = new RuleValueAny();
            billingRule9.PrimaryInsurance = new RuleValueAny();
            billingRule9.SecondaryInsurance = new RuleValueAny();
            billingRule9.PostDischarge = new RuleValueBoolean(true);
            billingRule9.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule9);
        }
    }
}
