using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class RuleValueString : RuleValue
    {
        protected string m_Value;

        public RuleValueString(string value)
        {
            this.m_Value = value;
        }

        public override bool IsMatch(object value)
        {
            bool result = false;
            string castedValue = (string)value;
            if (castedValue == this.m_Value) result = true;
            return result;
        }
    }
}
