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
            this.Add(new UnsatRule());
            this.Add(new UnsatRuleWithCotest());
            this.Add(new NormalRule());
            this.Add(new NormalWithCotestRule());
            this.Add(new AbnormalRule());
            this.Add(new AbnormalWithCotestRule());
            this.Add(new NoResultWithCotestRule());
            this.Add(new NoResultRule());
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
        
        public BaseRule GetMatchingRule(Woman woman)
        {
            BaseRule result = null;
            foreach(BaseRule rule in this)
            {
                if(rule.IsMatch(woman) == true)
                {
                    result = rule;
                    woman.RuleName = rule.Description;
                    woman.RuleIsMatch = true;
                    break;
                }
            }            
            return result;
        }
    }
}
