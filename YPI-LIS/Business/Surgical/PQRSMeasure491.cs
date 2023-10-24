using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure491 : PQRSMeasure
    {        
        public PQRSMeasure491()
        {            
			this.m_Header = "MIPS Measure 491";
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null));
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88309", null));
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.AllPatients;
            
            this.m_DiagnosisWordCollection.Add("Carcinoma");
            this.m_DiagnosisWordCollection.Add("Adenocarcinoma");
            
            this.m_SpecimenWordCollection.Add("colon");
            this.m_SpecimenWordCollection.Add("rectum");
            this.m_SpecimenWordCollection.Add("cecum");
            this.m_SpecimenWordCollection.Add("gastroesophageal");
            this.m_SpecimenWordCollection.Add("esophagus");
            this.m_SpecimenWordCollection.Add("stomach");
            this.m_SpecimenWordCollection.Add("endometrium");
            this.m_SpecimenWordCollection.Add("uterus");
            this.m_SpecimenWordCollection.Add("small bowel");
            this.m_SpecimenWordCollection.Add("duodenum");
            this.m_SpecimenWordCollection.Add("jejunum");
            this.m_SpecimenWordCollection.Add("ileum");
            
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("M1193", null));
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("M1194", null));
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("M1195", null));
        }

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;

            if (string.IsNullOrEmpty(surgicalSpecimen.SpecimenOrder.Description) == false)
            {
                if (WordsExistIn(surgicalSpecimen.SpecimenOrder.Description, this.m_SpecimenWordCollection) == true)
                {
                    if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false)
                    {
                        if (WordsExistIn(surgicalSpecimen.Diagnosis, this.m_DiagnosisWordCollection) == true)
                        {
                            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                            if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            
            return result;
        }        
    }
}
