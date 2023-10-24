using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRulePAPResult7 : HPVRule
    {
        public HPVRulePAPResult7()
        {
            this.m_Description = "ASCUS or higher";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;

            return result;
        }
    }
}
