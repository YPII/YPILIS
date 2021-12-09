using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure250 : PQRSMeasure
    {        
        public PQRSMeasure250()
        {            
			this.m_Header = "Radical Prostatectomy Pathology Reporting #250";

            this.m_PQRIKeyWordCollection.Add("prostate");
            this.m_PQRIKeyWordCollection.Add("resection");
            
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88309", null));

            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.AllPatients;            

            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("3267F", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("3267F", "1P"));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("3267F", "8P"));
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G8798", null));
        }

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false)
            {                                
                if (this.m_PQRIKeyWordCollection.WordsExistIn(surgicalSpecimen.Diagnosis) == true)
                {                    
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                    if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                    {
                        result = true;
                    }                    
                }
            }
            return result;
        }        
    }
}
