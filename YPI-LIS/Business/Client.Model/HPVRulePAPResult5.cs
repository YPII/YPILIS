using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    class HPVRulePAPResult5 : HPVRule
    {
        public HPVRulePAPResult5()
        {
            this.m_Description = "ASCUS, LSIL or HSIL";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;

            return result;
        }
    }
}
