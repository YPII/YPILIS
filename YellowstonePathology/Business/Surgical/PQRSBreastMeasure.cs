using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSBreastMeasure : PQRSMeasure
    {
        public PQRSBreastMeasure()
        {
            this.m_PQRIKeyWordCollection.Add("Breast");
			this.m_Header = "Breast Cancer Resection Pathology Reporting";
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88307());
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());

            this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3260F());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3260F1P());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3260F8P());
        }        
    }
}
