using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class NoResultWithCotestRule : BaseRule
    {        
        public NoResultWithCotestRule()
        {            
            this.m_Description = "No Result - with cotest";
        }

        public override bool IsMatch(Woman woman)
        {
            bool result = false;
            if(woman.OrderType.OrderCode == "11")
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
            woman.PerformHPV = true;
        }

        public override void FinalizeHPV(Woman woman)
        {
            woman.PerformHPV = true;
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            woman.PerformHPV = true;
        }
    }
}
