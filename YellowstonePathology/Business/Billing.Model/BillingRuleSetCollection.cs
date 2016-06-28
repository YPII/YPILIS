using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillingRuleSetCollection : ObservableCollection<BillingRuleSet>
    {
        public BillingRuleSetCollection()
        {

        }

        public static BillingRuleSetCollection GetAllRuleSets()
        {
            BillingRuleSetCollection result = new BillingRuleSetCollection();
            result.Add(new ClientBillingRuleSet());
            result.Add(new GlobalBillingRuleSet());
            result.Add(new HospitalSplitAllBillingRuleSet());
            result.Add(new HospitalGlobalBillingRuleSet());
            result.Add(new HospitalSplitProfessionalBillingRuleSet());
            result.Add(new NonProviderBasedClinicBillingRuleSet());                        
            result.Add(new YellowstoneParkBillingRuleSet());
            return result;
        }

        public static BillingRuleSet GetRuleSetByRuleSetId(string billingRuleSetId)
        {
            BillingRuleSetCollection billingRuleSetCollection = BillingRuleSetCollection.GetAllRuleSets();
            BillingRuleSet result = null;
            foreach (BillingRuleSet billingRuleSet in billingRuleSetCollection)
            {
                if (billingRuleSet.BillingRuleSetId == billingRuleSetId)
                {
                    result = billingRuleSet;
                    break;
                }
            }
            return result;
        }
    }
}
