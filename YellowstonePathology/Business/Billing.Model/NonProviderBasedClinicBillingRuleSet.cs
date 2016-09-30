using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class NonProviderBasedClinicBillingRuleSet : BillingRuleSet
    {
        public NonProviderBasedClinicBillingRuleSet()
        {
            this.m_BillingRuleSetId = "NPBCSPLT";
            this.m_BillingRuleSetIdOld = "187E6457-F908-41AF-8232-D7754CC3E0CC";
            this.m_BillingRuleSetName = "Non Provider Based Clinic Split";

            CptCodeCollection allCptCodes = CptCodeCollection.GetAll();
            CptCodeCollection clinicalFeeScheduleCodes = CptCodeCollection.GetCptCodeCollection(FeeScheduleEnum.Clinical);
            CptCodeCollection professionalFeeScheduleCodes = CptCodeCollection.GetCptCodeCollection(FeeScheduleEnum.Physician);

            BillingRule billingRule0 = new BillingRule();
            billingRule0.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule0.Priority = 0;            
            billingRule0.PatientType = new RuleValueAny();
            billingRule0.PrimaryInsurance = new RuleValueAny();
            billingRule0.SecondaryInsurance = new RuleValueAny();
            billingRule0.PostDischarge = new RuleValueAny();
            billingRule0.BillingType = BillingTypeEnum.Split;
            billingRule0.PanelSetIncludeOnlyList.Add(13);
            billingRule0.PanelSetIncludeOnlyList.Add(138);
            this.m_BillingRuleCollection.Add(billingRule0);

            BillingRule billingRule1 = new BillingRule();
            billingRule1.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule1.Priority = 1;            
            billingRule1.PatientType = new RuleValueAny();
            billingRule1.PrimaryInsurance = new RuleValueAny();
            billingRule1.SecondaryInsurance = new RuleValueAny();
            billingRule1.PostDischarge = new RuleValueAny();
            billingRule1.BillingType = BillingTypeEnum.Global;
            billingRule1.PanelSetExcludeList.Add(13); 
            this.m_BillingRuleCollection.Add(billingRule1);

            BillingRule billingRule2 = new BillingRule();
            billingRule2.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule2.Priority = 2;            
            billingRule2.PatientType = new RuleValueString("OP");
            billingRule2.PrimaryInsurance = new RuleValueString("Medicaid");
            billingRule2.SecondaryInsurance = new RuleValueAny();
            billingRule2.PostDischarge = new RuleValueAny();
            billingRule2.BillingType = BillingTypeEnum.Global;            
            this.m_BillingRuleCollection.Add(billingRule2);

            BillingRule billingRule3 = new BillingRule();
            billingRule3.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule3.Priority = 3;
            billingRule3.PatientType = new RuleValueString("OP");
            billingRule3.PrimaryInsurance = new RuleValueAny();
            billingRule3.SecondaryInsurance = new RuleValueString("Medicaid");            
            billingRule3.PostDischarge = new RuleValueAny();
            billingRule3.BillingType = BillingTypeEnum.Global;
            this.m_BillingRuleCollection.Add(billingRule3);

            BillingRule billingRule4 = new BillingRule();
            billingRule4.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule4.Priority = 4;            
            billingRule4.PatientType = new RuleValueAny();
            billingRule4.PrimaryInsurance = new RuleValueString("Medicare");
            billingRule4.SecondaryInsurance = new RuleValueAny();
            billingRule4.PostDischarge = new RuleValueAny();
            billingRule4.BillingType = BillingTypeEnum.Split;
            billingRule4.PanelSetIncludeOnlyList.Add(13);
            billingRule4.PanelSetIncludeOnlyList.Add(138);
            this.m_BillingRuleCollection.Add(billingRule4);            

            BillingRule billingRule5 = new BillingRule();
            billingRule5.BillingRuleSetId = this.m_BillingRuleSetId;
            billingRule5.Priority = 5;            
            billingRule5.PatientType = new RuleValueAny();
            billingRule5.PrimaryInsurance = new RuleValueAny();
            billingRule5.SecondaryInsurance = new RuleValueString("Governmental");
            billingRule5.PostDischarge = new RuleValueAny();
            billingRule5.BillingType = BillingTypeEnum.Split;
            billingRule5.PanelSetIncludeOnlyList.Add(13);
            billingRule4.PanelSetIncludeOnlyList.Add(138);
            this.m_BillingRuleCollection.Add(billingRule5);

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
