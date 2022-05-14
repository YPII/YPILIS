using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class NoMatchingRule : BaseRule
    {
        public NoMatchingRule()
        {
            this.m_Description = "No matching rule.";            
        }        
    }
}
