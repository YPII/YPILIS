using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillingRuleCollection : ObservableCollection<BillingRule>
	{
		public BillingRuleCollection()
		{

		}

        public BillingRule GetMatch(string patientType, string primaryInsurance, string secondaryInsurance, bool postDischarge, int panelSetId)
        {            
            BillingRuleCollection matchedBillingRules = new BillingRuleCollection();
            foreach (BillingRule billingRule in this)
            {
                if (billingRule.IsMatch(patientType, primaryInsurance, secondaryInsurance, postDischarge, panelSetId) == true)
                {
                    matchedBillingRules.Add(billingRule);
                }
            }

            if (matchedBillingRules.Count == 0) throw new Exception("There was no rule that matched.");
            BillingRule matchedRule = matchedBillingRules.GetRuleWithHighestPriority();

            return matchedRule;
        }

		public BillingRule GetRuleWithHighestPriority()
		{
			BillingRule result = this[0];
			foreach (BillingRule billingRule in this)
			{
				if (billingRule.Priority > result.Priority)
				{
					result = billingRule;
				}
			}
			return result;
		}				
	}
}
