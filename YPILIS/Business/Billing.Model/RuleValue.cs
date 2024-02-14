using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class RuleValue 
    {        
        public RuleValue()
        {
            
        }

        public virtual bool IsMatch(object value)
        {
            throw new Exception("Not Implemented Here.");
        }
    }
}
