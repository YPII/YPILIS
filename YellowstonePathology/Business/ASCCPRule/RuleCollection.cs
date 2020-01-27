using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class RuleCollection : ObservableCollection<BaseRule>
    {
        public RuleCollection()
        {
            this.Add(new Unsat());
            this.Add(new UnsatWithCotest());
            this.Add(new NILMECTZAbsent());
            this.Add(new NILMECTZAbsentWithCotest());
            this.Add(new Rule2());
            this.Add(new NoMatchingRule());
        }  

        public BaseRule GetNoMatchingRule()
        {
            BaseRule result = null;
            foreach (BaseRule rule in this)
            {
                if (rule.Description == "No matching rule.")
                {
                    result = rule;
                    break;
                }
            }

            return result;
        }
        
        public BaseRule GetMatchingRule()
        {
            BaseRule result = null;
            foreach(BaseRule rule in this)
            {
                if(rule.IsMatch == true)
                {
                    result = rule;
                    break;
                }
            }            
            
            if(result == null)
            {
                return this.GetNoMatchingRule();
            }
            else
            {
                return result;
            }            
        }
    }
}
