using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Cytology.Model;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class NILMECTZAbsent : BaseRule
    {        
        public NILMECTZAbsent()
        {            
            this.m_Description = "Cytology NILM but EC/TZ Absent/Insufficient";            
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

            //PAP Screening Only
            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();                
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");

                //this.FinalizePap(woman, satNoecc, nilm);
                //this.FinalizeHPV(woman, "Positive");
                //this.FinalizeGenotyping(woman, "Negative");
                result.Add(woman);
            }

            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");

                //this.FinalizePap(woman, satNoecc, nilm);
                //this.FinalizeHPV(woman, "Positive");
                //this.FinalizeGenotyping(woman, "Positive");
                result.Add(woman);
            }

            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");

                //this.FinalizePap(woman, satNoecc, nilm);
                //this.FinalizeHPV(woman, "Negative");
                //this.FinalizeGenotyping(woman, "Unknown");
                result.Add(woman);
            }            
            return result;
        }

        public override void FinalizePap(Woman woman)
        {
            if (woman.ScreeningImpression.ResultCode == "01" || woman.ScreeningImpression.ResultCode == "02")
            {
                if (woman.SpecimenAdequacy.ResultCode == "10")
                {
                    this.m_IsMatch = true;
                    if (woman.Age <= 29)
                    {
                        woman.PerformHPV = false;
                        woman.ReflexToHPVGenotypes = false;
                        woman.ManagementRecomendation = "No further testing is recommended.";
                    }
                    else if (woman.Age >= 30)
                    {
                        woman.PerformHPV = true;                                                                        
                    }
                }                 
            }
        }

        public override void FinalizeHPV(Woman woman)
        {            
            if(woman.PerformHPV == true)
            {
                if (woman.HPVResult == "Positive")
                {
                    woman.ReflexToHPVGenotypes = true;
                    woman.ManagementRecomendation = "Recommend colposcopy.";
                }
                else if(woman.HPVResult == "Negative")
                {
                    woman.ReflexToHPVGenotypes = false;
                    woman.ManagementRecomendation = "Recommend repeat cotesting in 3 years.";
                }                
            }            
        }

        public override void FinalizeGenotyping(Woman woman)
        {
            if (woman.ReflexToHPVGenotypes == true)
            {
                if (woman.HPVResult == "Positive")
                {
                    woman.ManagementRecomendation = "Recommend colposcopy.";
                }
                else if (woman.HPVResult == "Negative")
                {
                    woman.ManagementRecomendation = "Recommend repeat cotesting in 1 year.";
                }
            }
        }
    }
}
