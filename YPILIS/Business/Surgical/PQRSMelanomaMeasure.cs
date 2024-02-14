using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMelanomaMeasure : PQRSMeasure
    {
        private List<string> m_KeyWords;

        public PQRSMelanomaMeasure()
        {
            this.m_KeyWords = new List<string>();
            this.m_KeyWords.Add("MELANOMA");            
			this.m_Header = "Melanoma Reporting";
            this.m_CptCodeCollection.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null));
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.Patients18To75;

            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9428", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9429", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9430", null));
            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G9431", null));
        }

		public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder, 
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (this.CancerSummaryApplies(surgicalTestOrder) == true || this.DiagnosisApplies(surgicalSpecimen) == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                {
                    result = true;
                }
            }                        
            return result;
        }

        private bool CancerSummaryApplies(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalTestOrder.CancerSummary) == false)
            {
                foreach (string keyWord in this.m_KeyWords)
                {
                    if (surgicalTestOrder.CancerSummary.ToUpper().Contains(keyWord.ToUpper()) == true)
                    {                        
                        result = true;
                        break;                        
                    }
                }
            }
            return result;
        }

        private bool DiagnosisApplies(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false)
            {
                foreach (string keyWord in this.m_KeyWords)
                {
                    if (surgicalSpecimen.Diagnosis.ToUpper().Contains(keyWord.ToUpper()) == true)
                    {                        
                        result = true;
                        break;                        
                    }
                }
            }
            return result;
        }
    }
}
