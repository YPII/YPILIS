﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    class HPVRulePAPResult2 : HPVRule
    {
        public HPVRulePAPResult2()
        {
            this.m_Description = "ASCUS";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;

            return result;
        }
    }
}
