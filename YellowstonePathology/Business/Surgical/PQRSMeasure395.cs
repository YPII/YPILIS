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
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305());
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88307());
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.Patients18To75;

            this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG9418());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG9419());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG9420());
            this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG9421());
        }

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.SpecimenOrder.Description) == false)
            {
                string specimenKeyWord = "Lung";
                if (surgicalSpecimen.SpecimenOrder.Description.ToUpper().Contains(specimenKeyWord.ToUpper()) == true)
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
