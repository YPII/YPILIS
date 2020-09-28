using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class NoResultRule : BaseRule
    {        
        public NoResultRule()
        {            
            this.m_Description = "No Result";
        }

        public override bool IsMatch(Woman woman)
        {
            bool result = false;
            if(woman.OrderType.OrderCode == "10")
            {
                if(woman.SpecimenAdequacy == null && woman.ScreeningImpression == null)
                {
                    result = true;
                }
            }            
            return result;
        }

        public override void FinalizePap(Woman woman)
        {
            woman.PerformHPV = false;
        }

        public override void FinalizeHPV(Woman woman)
        {
            woman.PerformHPV = true;
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            woman.PerformHPV = false;
        }
    }
}
