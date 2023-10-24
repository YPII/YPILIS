using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    class HPVRulePAPResult3 : HPVRule
    {
        public HPVRulePAPResult3()
        {
            this.m_Description = "ASCUS or LSIL";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;

            return result;
        }
    }
}
