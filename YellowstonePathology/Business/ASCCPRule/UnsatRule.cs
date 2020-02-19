using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class UnsatRule : BaseRule
    {
        public UnsatRule()
        {            
            this.m_Description = "Unsatisfactory Cytology";                                                   
        }

        public override bool IsMatch(Woman woman)
        {
            bool result = false;
            if (woman.SpecimenAdequacy.ResultCode == "25" && woman.OrderType.OrderCode == "10")
            {
                result = true;
            }
            return result;
        }

        public override void FinalizePap(Woman woman)
        {                                                    
            woman.ManagementRecommendation = ManagementRecomendation.GetRepeatTwoToFour();            
        }

        public override void FinalizeHPV(Woman woman)
        {
            //Do nothing
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            //Do nothing
        }
    }
}
