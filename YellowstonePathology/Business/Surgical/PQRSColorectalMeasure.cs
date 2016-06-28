using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
	public class PQRSColorectalMeasure : PQRSMeasure
	{
		public PQRSColorectalMeasure()
        {            
            this.m_PQRIKeyWordCollection.Add("Cecum");
			this.m_PQRIKeyWordCollection.Add("Colon");
            this.m_PQRIKeyWordCollection.Add("Sigmoid");
            this.m_PQRIKeyWordCollection.Add("Rectum");
            this.m_PQRIKeyWordCollection.Add("Apendix");
            this.m_PQRIKeyWordCollection.Add("Appendix");

            this.m_Header = "Colorectal Cancer Pathology Reporting";
            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());

			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG8721());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG8722());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG8723());
		}        
	}
}
