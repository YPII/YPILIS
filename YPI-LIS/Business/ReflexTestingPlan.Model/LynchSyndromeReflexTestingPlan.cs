using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReflexTestingPlan.Model
{
    public class LynchSyndromeReflexTestingPlan : ReflexTestingPlan
    {
        public LynchSyndromeReflexTestingPlan(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder, new LynchIHCStep())
        {
            this.m_Name = "Lynch Syndrome Testing";            
        }
    }
}
