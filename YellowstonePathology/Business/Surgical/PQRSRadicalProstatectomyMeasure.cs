using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
	public class PQRSRadicalProstatectomyMeasure : PQRSMeasure
	{
		public PQRSRadicalProstatectomyMeasure()
        {
			this.m_PQRIKeyWordCollection.Add("Prostate");
			this.m_Header = "Radical Prostatectomy Pathology Reporting";

            this.m_CptCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309());

			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3267F());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3267F1P());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRS3267F8P());
			this.m_PQRSCodeCollection.Add(new YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions.PQRSG8798());
		}        
	}
}
