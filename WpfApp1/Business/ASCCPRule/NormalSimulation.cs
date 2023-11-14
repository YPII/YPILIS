using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class NormalSimulation : Simulation
    {
        public NormalSimulation() 
            : base(new NormalRule())
        {
            
        }

        public override void Run()
        {            
            Business.Cytology.Model.SpecimenAdequacyCollection specimenAdequacyCollection = Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();            
            Business.Cytology.Model.SpecimenAdequacy satNoecc = specimenAdequacyCollection.Get("10");
            Business.Cytology.Model.SpecimenAdequacy satEcc = specimenAdequacyCollection.Get("11");

            Business.Cytology.Model.OrderTypeCollection orderTypeCollection = new Cytology.Model.OrderTypeCollection();            
            Business.Cytology.Model.ScreeningImpression nilm = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("01");
            
            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");
                woman.SpecimenAdequacy = satNoecc;
                woman.ScreeningImpression = nilm;
                woman.ECTZAbsent = true;
                this.m_Rule.FinalizePap(woman);

                if(this.m_Rule.IsMatch(woman) == true)
                {
                    if (woman.PerformHPV == true)
                    {
                        woman.HPVResult = "Positive";
                        this.m_Rule.FinalizeHPV(woman);

                        if (woman.ReflexToHPVGenotypes == true)
                        {
                            woman.HPV16Result = "Negative";
                            woman.HPV18Result = "Negative";
                            this.m_Rule.FinalizeGenotyping(woman);
                        }
                    }
                    this.m_WomanCollection.Add(woman);
                }                
            }

            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");
                woman.SpecimenAdequacy = satNoecc;
                woman.ScreeningImpression = nilm;
                woman.ECTZAbsent = true;
                this.m_Rule.FinalizePap(woman);

                if (this.m_Rule.IsMatch(woman) == true)
                {
                    if (woman.PerformHPV == true)
                    {
                        woman.HPVResult = "Positive";
                        this.m_Rule.FinalizeHPV(woman);

                        if (woman.ReflexToHPVGenotypes == true)
                        {
                            woman.HPV16Result = "Positive";
                            woman.HPV18Result = "Negative";
                            this.m_Rule.FinalizeGenotyping(woman);
                        }
                    }
                    this.m_WomanCollection.Add(woman);
                }
            }

            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");
                woman.SpecimenAdequacy = satEcc;
                woman.ScreeningImpression = nilm;
                woman.ECTZAbsent = false;

                if(this.m_Rule.IsMatch(woman) == true)
                {
                    this.m_Rule.FinalizePap(woman);
                    if (woman.PerformHPV == true)
                    {
                        woman.HPVResult = "Positive";
                        this.m_Rule.FinalizeHPV(woman);

                        if (woman.ReflexToHPVGenotypes == true)
                        {
                            woman.HPV16Result = "Negative";
                            woman.HPV18Result = "Negative";
                            this.m_Rule.FinalizeGenotyping(woman);
                        }
                    }
                    this.m_WomanCollection.Add(woman);
                }                
            }            
        }
    }
}
