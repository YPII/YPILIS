using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
	public class PQRSBarrettsEsophagusMeasure : PQRSMeasure
	{
        private List<string> m_KeyWords;

		public PQRSBarrettsEsophagusMeasure()
        {
            this.m_KeyWords = new List<string>();
            this.m_KeyWords.Add("Barret");
            this.m_KeyWords.Add("Intestinal Metaplasia");
			
			this.m_Header = "Barretts Esophagus Pathology Reporting";
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305());

			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3126F());
            this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3126F1P());
            this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG8797());			
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3126F8P());			
		}

        public override bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;
            if (string.IsNullOrEmpty(surgicalSpecimen.SpecimenOrder.Description) == false)
            {
                foreach (string keyWord in this.m_KeyWords)
                {
                    if (string.IsNullOrEmpty(surgicalSpecimen.Diagnosis) == false &&
                        surgicalSpecimen.Diagnosis.ToUpper().Contains(keyWord.ToUpper()) == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                        if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }        
	}
}
