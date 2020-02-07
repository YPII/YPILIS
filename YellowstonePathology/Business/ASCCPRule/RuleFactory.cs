using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class RuleFactory
    {
        public static BaseRule GetUnsatRule(Woman woman)
        {
            if (woman.OrderType.OrderCode == "10")
            {
                return new Unsat();
            } 
            else if (woman.OrderType.OrderCode == "11")
            {
                return new UnsatWithCotest();
            }
            else
            {
                return null;
            }
        }
    }
}
