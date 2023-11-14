using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAge6 : HPVRule
    {
        public HPVRuleAge6()
        {
            this.m_Description = "between 21 and 24 years old";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = accessionOrder.PBirthdate >= DateTime.Today.AddYears(-24) && accessionOrder.PBirthdate <= DateTime.Today.AddYears(-21) ? true : false;

            return result;
        }
    }
}
