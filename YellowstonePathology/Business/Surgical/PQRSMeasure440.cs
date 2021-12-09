using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure440 : PQRSMeasure
    {        
        public PQRSMeasure440()
        {            
			this.m_Header = "Basal Cell Carcinoma (BCC)/ Squamous Cell Carcinoma (SCC) or Melanoma including in situ #440";
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", null));
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null));
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.AllPatients;

            this.m_PQRIKeyWordCollection.Add("Basal Cell Carcinoma");
            this.m_PQRIKeyWordCollection.Add("Squamous Cell Carcinoma");
            this.m_PQRIKeyWordCollection.Add("Melanoma");

            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9785", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9786", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9784", null));
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9939", null));
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
