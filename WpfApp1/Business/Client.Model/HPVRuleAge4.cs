using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge4 : HPVRule
    {
        public HPVRuleAge4()
        {
            this.m_Description = "older than 20";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = accessionOrder.PBirthdate < DateTime.Today.AddYears(-20) ? true : false;
            return result;
        }
    }
}
