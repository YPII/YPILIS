using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Validation
{
    public delegate void ValidationRuleDelegate(object value, BrokenRuleCollection rules);

    public class ValidationRuleItem
    {        
        string m_PropertyName;
        ValidationRuleDelegate m_ValidationRuleHandler;

        public ValidationRuleItem(ValidationRuleDelegate del)
        {
            this.m_ValidationRuleHandler = del;
        }        

        public string PropertyName
        {
            get { return this.m_PropertyName; }
            set { this.m_PropertyName = value; }
        }

        public void Validate(object value, BrokenRuleCollection rules)
        {
            this.m_ValidationRuleHandler(value, rules);
        }
    }
}
