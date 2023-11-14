using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class RuleValueAny : RuleValue
    {
        public RuleValueAny()
        {

        }

        public override bool IsMatch(object value)
        {
            return true;
        }
    }
}
