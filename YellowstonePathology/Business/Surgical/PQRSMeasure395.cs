using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure395 : PQRSMeasure
    {        
        public PQRSMeasure395()
        {            
			this.m_Header = "Lung Cancer Reporting #395";

            this.m_DiagnosisWordCollection.Add("Lung");
            this.m_DiagnosisWordCollection.Add("Bronch");
            this.m_DiagnosisWordCollection.Add("Pleural");

            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88104", null));
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88108", null));
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88112", null));
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88173", null));
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null));

            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.Patients18AndOlder;            

            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9418", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9419", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9420", null));
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9421", null));
        }

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.SpecimenOrder.Description) == false)
            {                                
                if (WordsExistIn(surgicalSpecimen.SpecimenOrder.Description, this.m_DiagnosisWordCollection) == true)
                {
                    string diagnosisKeyWord = "Carcinoma";
                    if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false && surgicalSpecimen.Diagnosis.ToUpper().Contains(diagnosisKeyWord.ToUpper()) == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                        if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }        
    }
}
