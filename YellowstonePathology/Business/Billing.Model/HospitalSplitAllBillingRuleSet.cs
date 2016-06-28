using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class HospitalSplitAllBillingRuleSet : BillingRuleSet
    {
        public HospitalSplitAllBillingRuleSet()
        {            
            this.m_BillingRuleSetId = "HSA";
            this.m_BillingRuleSetIdOld = "6040FB9A-6D25-4E3E-9922-A48BB8CEC8E3";
            this.m_BillingRuleSetName = "Hospital Split All Rule Set";

            CptCodeCollection allCptCodes = CptCodeCollection.GetAll();

            BillingRule billingRule0 = new BillingRule();
            billingRule0.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule0.Priority = 0;            
            billingRule0.PatientType = new RuleValueAny();
            billingRule0.PrimaryInsurance = new RuleValueAny();
            billingRule0.SecondaryInsurance = new RuleValueAny();
            billingRule0.PostDischarge = new RuleValueAny();
            billingRule0.BillingType = BillingTypeEnum.Split;
            this.m_BillingRuleCollection.Add(billingRule0);

            BillingRule billingRule1 = new BillingRule();
            billingRule1.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule1.Priority = 1;
			billingRule1.PatientType = new RuleValueString("OP");
			billingRule1.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule1.SecondaryInsurance = new RuleValueAny();
            billingRule1.PostDischarge = new RuleValueAny();
            billingRule1.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule1);

            BillingRule billingRule2 = new BillingRule();
            billingRule2.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule2.Priority = 2;
            billingRule2.PatientType = new RuleValueString("OP");            
            billingRule2.PrimaryInsurance = new RuleValueAny();
            billingRule2.SecondaryInsurance = new RuleValueString("Medicaid");
            billingRule2.PostDischarge = new RuleValueAny();
            billingRule2.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule2);

            BillingRule billingRule3 = new BillingRule();
            billingRule3.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule3.Priority = 3;            
            billingRule3.PatientType = new RuleValueAny();
            billingRule3.PrimaryInsurance = new RuleValueAny();
            billingRule3.SecondaryInsurance = new RuleValueAny();
			billingRule3.PostDischarge = new RuleValueBoolean(true);
            billingRule3.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule3);            
        }
    }
}
