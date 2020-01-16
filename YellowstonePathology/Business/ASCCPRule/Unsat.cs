using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class Unsat : BaseRule
    {
        public Unsat()
        {            
            this.m_Description = "Unsatisfactory Cytology";                                                   
        }

        public override WomanCollection RunSimulation()
        {
            WomanCollection result = new WomanCollection();
            Business.Cytology.Model.SpecimenAdequacyCollection specimenAdequacyCollection = Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();
            Business.Cytology.Model.SpecimenAdequacy unsat = specimenAdequacyCollection.Get("25");
            Business.Cytology.Model.SpecimenAdequacy satNoecc = specimenAdequacyCollection.Get("10");

            Business.Cytology.Model.OrderTypeCollection orderTypeCollection = new OrderTypeCollection();
            Business.Cytology.Model.ScreeningImpression noImpression = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("00");
            Business.Cytology.Model.ScreeningImpression nilm = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("01");

            for (int i=28; i<=32; i++)
            {
                Woman woman = new Woman();
                woman.Name = "WMN" + i;
                woman.Age = i;                
                woman.OrderType = orderTypeCollection.Get("10");                

                this.FinalizePap(woman, unsat, noImpression);
                this.FinalizeHPV(woman, "Unknown");
                this.FinalizeGenotyping(woman, "Unknown");
                result.Add(woman);               
            }           

            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Name = "WMN" + i;
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");
                woman.HPVIsOrdered = true;

                this.FinalizePap(woman, satNoecc, nilm);
                this.FinalizeHPV(woman, "Unknown");
                this.FinalizeGenotyping(woman, "Unknown");
                result.Add(woman);
            }
            return result;
        }

        public override void FinalizePap(Woman woman, SpecimenAdequacy specimenAdequacy, ScreeningImpression screeningImpression)
        {
            woman.SpecimenAdequacy = specimenAdequacy;
            woman.ScreeningImpression = screeningImpression;

            if (specimenAdequacy.ResultCode == "25")
            {
                this.m_IsMatch = true;
                woman.RuleIsMatch = true;                

                if (woman.OrderType.OrderCode == "10")
                {                    
                    this.m_RepeatTestingInterval = "2-4 months";
                    woman.ManagementRecomendation = "Recomend repeat cytology in 2-4 months.";                  
                }                                                
            }            
        }

        public override void FinalizeHPV(Woman woman, string result)
        {
            woman.HPVResult = result;
            if (woman.OrderType.OrderCode == "11")
            {                
                this.m_RepeatTestingInterval = "2-4 months.";
                if(woman.Age >= 30)
                {
                    switch (result)
                    {
                        case "Negative":
                            woman.ManagementRecomendation = "Recomend repeat cytology in 2-4 months.";
                            break;
                        case "Positive":
                            woman.ManagementRecomendation = "Recomend colposcopy or repeat cytology in 2-4 months.";
                            break;
                        //default:
                        //    throw new Exception("This result not handled.");
                    }
                }
                else
                {
                    woman.ManagementRecomendation = "Recomend repeat cytology in 2-4 months.";
                }     
            }
        }

        public override void FinalizeGenotyping(Woman woman, string result)
        {
            woman.GenotypingResult = result;
            //Do nothing
        }
    }
}
