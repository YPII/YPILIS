using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class YellowstoneParkBillingRuleSet : BillingRuleSet
    {
        public YellowstoneParkBillingRuleSet()
        {            
            this.m_BillingRuleSetId = "YLWSTN";
            this.m_BillingRuleSetIdOld = "BCFC243C-8608-4F09-B8C7-173508A07BAD";
            this.m_BillingRuleSetName = "Yellowstone Park Rule Set";

            CptCodeCollection allCptCodes = CptCodeCollection.GetAll();

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
            billingRule1.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule1.SecondaryInsurance = new RuleValueAny();
            billingRule1.PostDischarge = new RuleValueAny();
            billingRule1.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule1);

            BillingRule billingRule2 = new BillingRule();
            billingRule2.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule2.Priority = 2;            
            billingRule2.PatientType = new RuleValueAny();
            billingRule2.PrimaryInsurance = new RuleValueString("Medicare");
            billingRule2.SecondaryInsurance = new RuleValueAny();
            billingRule2.PostDischarge = new RuleValueAny();
            billingRule2.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule2);

            BillingRule billingRule3 = new BillingRule();
            billingRule3.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule3.Priority = 3;            
            billingRule3.PatientType = new RuleValueAny();
            billingRule3.PrimaryInsurance = new RuleValueString("Commercial");
            billingRule3.SecondaryInsurance = new RuleValueAny();
            billingRule3.PostDischarge = new RuleValueAny();
            billingRule3.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule3);

            //this.m_BillingRuleCollection.Add(new BillingRule("YLWSTN", 1, "Any", "Medicaid", "Any", "Any", YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Global, "All Codes", allCptCodes));
            //this.m_BillingRuleCollection.Add(new BillingRule("YLWSTN", 1, "Any", "Medicare", "Any", "Any", YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Global, "All Codes", allCptCodes));
            //this.m_BillingRuleCollection.Add(new BillingRule("YLWSTN", 0, "Any", "Any", "Any", "Any", YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Client, "All Codes", allCptCodes));
            //this.m_BillingRuleCollection.Add(new BillingRule("YLWSTN", 1, "Any", "Commercial", "Any", "Any", YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Global, "All Codes", allCptCodes));
        }
    }
}
