using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class UnsatWithCotestSimulation : Simulation
    {
        public UnsatWithCotestSimulation() 
            : base(new UnsatRuleWithCotest())
        {
            
        }

        public override void Run()
        {            
            Business.Cytology.Model.SpecimenAdequacyCollection specimenAdequacyCollection = Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();
            Business.Cytology.Model.SpecimenAdequacy unsat = specimenAdequacyCollection.Get("25");            

            Business.Cytology.Model.OrderTypeCollection orderTypeCollection = new Cytology.Model.OrderTypeCollection();
            Business.Cytology.Model.ScreeningImpression noImpression = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("00");            

            for (int i = 28; i <= 32; i++)
            {
                Woman woman = new Woman();
                woman.Name = "WMN" + i;
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("11");
                woman.PerformHPV = true;
                woman.SpecimenAdequacy = unsat;
                woman.ScreeningImpression = noImpression;
                
                if(this.m_Rule.IsMatch(woman) == true)
                {
                    this.m_Rule.FinalizePap(woman);
                    woman.HPVResult = "Positive";
                    this.m_Rule.FinalizeHPV(woman);
                    this.m_Rule.FinalizeGenotyping(woman);
                    this.m_WomanCollection.Add(woman);
                }                
            }            
        }
    }
}
