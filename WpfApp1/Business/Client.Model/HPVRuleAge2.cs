using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge2 : HPVRule
    {
        public HPVRuleAge2()
        {
            this.m_Description = "older than 30";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = accessionOrder.PBirthdate < DateTime.Today.AddYears(-30) ? true : false;
            return result;
        }
    }
}
