using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class AbnormalWithCotestRule : BaseRule
    {        
        public AbnormalWithCotestRule()
        {
            this.m_Description = "Abnormal Cytology - with cotest";            
        }

        public override bool IsMatch(Woman woman)
        {
            bool result = false;
            if( int.Parse(woman.ScreeningImpression.ResultCode) >= 3 &&
                woman.OrderType.OrderCode == "11")                
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
            if (woman.Age >= 21 && woman.Age <= 24)
            {
                if (woman.ScreeningImpression.ResultCode == "03" ||
                    woman.ScreeningImpression.ResultCode == "05")
                {                    
                    woman.ManagementRecommendation = ManagementRecomendation.GetRepeatIn1Year();
                }
                else if (woman.ScreeningImpression.ResultCode == "04" ||
                    woman.ScreeningImpression.ResultCode == "07" ||
                    woman.ScreeningImpression.ResultCode == "06")
                {                    
                    woman.ManagementRecommendation = ManagementRecomendation.GetColposcopy();
                }
                else
                {
                    throw new Exception("Not handled.");
                }
            }
            else if (woman.Age >= 25)
            {
                if (woman.ScreeningImpression.ResultCode == "03")
                {
                    if(woman.HPVResult == "Positive")
                    {
                        woman.ManagementRecommendation = ManagementRecomendation.GetColposcopy();
                    }
                    else if(woman.HPVResult == "Negative")
                    {
                        woman.ManagementRecommendation = ManagementRecomendation.GetRoutineScreening();
                    }
                    else
                    {
                        throw new Exception("Not Handled.");
                    }
                }
                else if (woman.ScreeningImpression.ResultCode == "04" ||
                    woman.ScreeningImpression.ResultCode == "05" ||
                    woman.ScreeningImpression.ResultCode == "06" ||
                    woman.ScreeningImpression.ResultCode == "07")
                {                    
                    woman.ManagementRecommendation = ManagementRecomendation.GetColposcopy();
                }                
                else
                {
                    throw new Exception("not handled.");
                }
            }
            else
            {
                throw new Exception("Not handled.");
            }
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            // do nothing.
        }
    }
}
