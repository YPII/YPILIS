using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ClientBillingRuleSet : BillingRuleSet
    {
        public ClientBillingRuleSet()
        {
            this.m_BillingRuleSetId = "CLNT";
            this.m_BillingRuleSetIdOld = "B3A017F0-84C4-471D-8510-3ABC3DB9DE53";
            this.m_BillingRuleSetName = "Client Billing Rule Set";

            BillingRule billingRule0 = new BillingRule();
            billingRule0.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule0.Priority = 0;            
            billingRule0.PatientType = new RuleValueAny();
            billingRule0.PrimaryInsurance = new RuleValueAny();
            billingRule0.SecondaryInsurance = new RuleValueAny();
            billingRule0.PostDischarge = new RuleValueAny();
            billingRule0.BillingType = BillingTypeEnum.Client;
            this.m_BillingRuleCollection.Add(billingRule0);

            BillingRule billingRule1 = new BillingRule();
            billingRule1.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule1.Priority = 1;            
            billingRule1.PatientType = new RuleValueAny();
            billingRule1.PrimaryInsurance = new RuleValueString("Commercial");
            billingRule1.SecondaryInsurance = new RuleValueAny();
            billingRule1.PostDischarge = new RuleValueAny();
            billingRule1.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule1);

            BillingRule billingRule2 = new BillingRule();
            billingRule2.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule2.Priority = 2;            
            billingRule2.PatientType = new RuleValueAny();
            billingRule2.PrimaryInsurance = new RuleValueString("BCHP");
            billingRule2.SecondaryInsurance = new RuleValueAny();
            billingRule2.PostDischarge = new RuleValueAny();
            billingRule2.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule2);

            BillingRule billingRule3 = new BillingRule();
            billingRule3.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule3.Priority = 3;            
            billingRule3.PatientType = new RuleValueAny();
            billingRule3.PrimaryInsurance = new RuleValueString("Medicare");
            billingRule3.SecondaryInsurance = new RuleValueAny();
            billingRule3.PostDischarge = new RuleValueAny();
            billingRule3.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule3);

            BillingRule billingRule4 = new BillingRule();
            billingRule4.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule4.Priority = 4;            
            billingRule4.PatientType = new RuleValueAny();
            billingRule4.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule4.SecondaryInsurance = new RuleValueAny();
            billingRule4.PostDischarge = new RuleValueAny();
            billingRule4.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule4);

            BillingRule billingRule41 = new BillingRule();
            billingRule41.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule41.Priority = 4;
            billingRule41.PatientType = new RuleValueAny();
            billingRule41.PrimaryInsurance = new RuleValueString("Governmental");
            billingRule41.SecondaryInsurance = new RuleValueAny();
            billingRule41.PostDischarge = new RuleValueAny();
            billingRule41.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule41);

            BillingRule billingRule5 = new BillingRule();
            billingRule5.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule5.Priority = 5;            
            billingRule5.PatientType = new RuleValueAny();
            billingRule5.PrimaryInsurance = new RuleValueAny();
            billingRule5.SecondaryInsurance = new RuleValueString("BCHP");
            billingRule5.PostDischarge = new RuleValueAny();
            billingRule5.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule5);

            BillingRule billingRule6 = new BillingRule();
            billingRule6.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule6.Priority = 6;            
            billingRule6.PatientType = new RuleValueAny();
            billingRule6.PrimaryInsurance = new RuleValueAny();
            billingRule6.SecondaryInsurance = new RuleValueString("Medicare");
            billingRule6.PostDischarge = new RuleValueAny();
            billingRule6.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule6);

            BillingRule billingRule7 = new BillingRule();
            billingRule7.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule7.Priority = 7;            
            billingRule7.PatientType = new RuleValueAny();
            billingRule7.PrimaryInsurance = new RuleValueAny();
            billingRule7.SecondaryInsurance = new RuleValueString("Medicaid");
            billingRule7.PostDischarge = new RuleValueAny();
            billingRule7.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule7);            
        }
    }
}
