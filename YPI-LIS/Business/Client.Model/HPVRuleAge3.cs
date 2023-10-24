using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge3 : HPVRule
    {
        public HPVRuleAge3()
        {
            this.m_Description = "25 and older";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = accessionOrder.PBirthdate <= DateTime.Today.AddYears(-25) ? true : false;
            return result;
        }
    }
}
