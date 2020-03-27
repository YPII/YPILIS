using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class NormalWithCotestRule : BaseRule
    {        
        public NormalWithCotestRule()
        {            
            this.m_Description = "Normal Cytology - with cotest";
        }

        public override bool IsMatch(Woman woman)
        {
            bool result = false;
            if(woman.OrderType.OrderCode == "11")
            {
                if (woman.SpecimenAdequacy.ResultCode == "10" ||
                    woman.SpecimenAdequacy.ResultCode == "11" ||
                    woman.SpecimenAdequacy.ResultCode == "15")
                {
                    if (woman.ScreeningImpression.ResultCode == "01" || 
                        woman.ScreeningImpression.ResultCode == "02")
                    {
                        result = true;
                    }
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
            if(woman.PerformHPV == true)
            {
                if (woman.Age >= 30 && woman.HPVResult == "Positive")
                {
                    woman.ReflexToHPVGenotypes = true;                    
                }
                else
                {
                    woman.ReflexToHPVGenotypes = false;
                    woman.ManagementRecommendation = ManagementRecomendation.GetRoutineScreening();
                }                              
            }            
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            if (woman.ReflexToHPVGenotypes == true)
            {
                if(woman.IsGenotypesPositive() == true)
                {
                    woman.ManagementRecommendation = ManagementRecomendation.GetColposcopy();
                }
                else
                {
                    woman.ManagementRecommendation = ManagementRecomendation.GetRepeatInOneYear(woman.OrderType.OrderCode);
                }                
            }
        }
    }
}
