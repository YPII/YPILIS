using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class AbnormalSimulation : Simulation
    {
        public AbnormalSimulation() 
            : base(new AbnormalRule())
        {
            
        }

        public override void Run()
        {
            Business.Cytology.Model.ScreeningImpression ascus = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("03");
            Business.Cytology.Model.ScreeningImpression lsil = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("05");
            Business.Cytology.Model.ScreeningImpression agc = Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("07");

            SetupWithDiagnosis(ascus, "Positive");
            SetupWithDiagnosis(ascus, "Negative");
            SetupWithDiagnosis(lsil, "Positive");
            SetupWithDiagnosis(agc, "Positive");
        }

        public void SetupWithDiagnosis(Business.Cytology.Model.ScreeningImpression screeningImpression, string hpvResult)
        {
            Business.Cytology.Model.SpecimenAdequacyCollection specimenAdequacyCollection = Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();
            Business.Cytology.Model.SpecimenAdequacy satNoecc = specimenAdequacyCollection.Get("10");

            Business.Cytology.Model.OrderTypeCollection orderTypeCollection = new Cytology.Model.OrderTypeCollection();            

            for (int i = 21; i <= 28; i++)
            {
                Woman woman = new Woman();
                woman.Name = "WMN" + i;
                woman.Age = i;
                woman.OrderType = orderTypeCollection.Get("10");
                woman.SpecimenAdequacy = satNoecc;
                woman.ScreeningImpression = screeningImpression;

                if (this.m_Rule.IsMatch(woman) == true)
                {
                    this.m_Rule.FinalizePap(woman);
                    if (woman.PerformHPV == true)
                    {
                        woman.HPVResult = hpvResult;
                        this.m_Rule.FinalizeHPV(woman);
                        this.m_Rule.FinalizeGenotyping(woman);
                    }
                    this.m_WomanCollection.Add(woman);
                }
            }
        }
    }
}
