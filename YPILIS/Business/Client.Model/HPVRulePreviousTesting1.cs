using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    class HPVRulePreviousTesting1 : HPVRule
    {
        public HPVRulePreviousTesting1()
        {
            this.m_Description = "Any";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return true;
        }
    }
}
