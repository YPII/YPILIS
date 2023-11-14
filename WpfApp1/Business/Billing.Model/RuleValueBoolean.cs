using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class RuleValueBoolean : RuleValue
    {
        protected bool m_Value;

        public RuleValueBoolean(bool value) 
        {
            this.m_Value = value;
        }

        public override bool IsMatch(object value)
        {
            bool result = false;
            bool castedValue = (bool)value;
            if (castedValue == this.m_Value) result = true;
            return result;
        }
    }
}
