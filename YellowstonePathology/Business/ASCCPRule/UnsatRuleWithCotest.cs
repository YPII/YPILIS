using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class UnsatRuleWithCotest : BaseRule
    {
        public UnsatRuleWithCotest()
        {            
            this.m_Description = "Unsatisfactory Cytology - with cotest";                                                   
        }

        public override bool IsMatch(Woman woman)
        {
            bool result = false;
            if (woman.SpecimenAdequacy.ResultCode == "25" && woman.OrderType.OrderCode == "11")
            {
                result = true;
            }
            return result;
        }

        public override void FinalizePap(Woman woman)
        {
            woman.PerformHPV = true;
        }

        public override void FinalizeHPV(Woman woman)
        {                        
            if(woman.Age >= 30)
            {
                switch (woman.HPVResult)
                {
                    case "Negative":
                        woman.ManagementRecommendation = ManagementRecomendation.GetRepeatTwoToFour();
                        break;
                    case "Positive":
                        woman.ManagementRecommendation = ManagementRecomendation.GetColposcopyOrRepeatTwoToFour();
                        break;
                }
            }
            else
            {
                woman.ManagementRecommendation = ManagementRecomendation.GetRepeatTwoToFour();
            }                 
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            //Do nothing
        }
    }
}
