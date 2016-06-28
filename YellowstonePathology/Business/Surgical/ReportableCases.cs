using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
	class ReportableCases
	{
        List<string> m_ImportantStrings;

		public ReportableCases()
        {
            this.m_ImportantStrings = new List<string>();
            this.SetImportStringList();
        }

		public bool IsPossibleReportableCase(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder testOrder)
        {
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen item in testOrder.SurgicalSpecimenCollection)
            {
				if(!string.IsNullOrEmpty(item.SpecimenType) && !string.IsNullOrEmpty(item.Diagnosis))
				{
					if (item.SpecimenType.ToUpper().IndexOf("SKIN") == -1 | item.Diagnosis.ToUpper().IndexOf("MELANOMA") >= 0)
					{
						foreach (string str in this.m_ImportantStrings)
						{
							if (item.Diagnosis.ToUpper().IndexOf(str.ToUpper()) > -1)
							{
								return true;                        
							}
						}
					}
				}
            }
            return false;
        }

        public void SetImportStringList()
        {
            this.m_ImportantStrings.Add("Brain");
            this.m_ImportantStrings.Add("Carcinoma");
            this.m_ImportantStrings.Add("Merkel");
            this.m_ImportantStrings.Add("Myelofibrosis");            
            this.m_ImportantStrings.Add("Rhabdoid");
            this.m_ImportantStrings.Add("Plasmacytoma");
            this.m_ImportantStrings.Add("Melanoma");
            this.m_ImportantStrings.Add("Leukemia");
            this.m_ImportantStrings.Add("Lymphoma");
            this.m_ImportantStrings.Add("Hodgkin");
            this.m_ImportantStrings.Add("Carcinoid");
            this.m_ImportantStrings.Add("Meningioma");
            this.m_ImportantStrings.Add("In-situ");
            this.m_ImportantStrings.Add("Glioma");
            this.m_ImportantStrings.Add("Astrocytoma");
            this.m_ImportantStrings.Add("Glioblastoma");
            this.m_ImportantStrings.Add("Malignan*");
            this.m_ImportantStrings.Add("Pituitary Adenoma");
            this.m_ImportantStrings.Add("Oligodendroglioma");
            this.m_ImportantStrings.Add("Schwannoma");
            this.m_ImportantStrings.Add("Eppendymoma");
            this.m_ImportantStrings.Add("Neuroma");            
            this.m_ImportantStrings.Add("CIS");
            this.m_ImportantStrings.Add("CIN III");
            this.m_ImportantStrings.Add("CIN 3");
            this.m_ImportantStrings.Add("Prostatic Intraepithelial Neoplasia");
            this.m_ImportantStrings.Add("PIN III");
            this.m_ImportantStrings.Add("PIN 3");
            this.m_ImportantStrings.Add("Vulva Intraepithelial Neoplasia");
            this.m_ImportantStrings.Add("VIN III");
            this.m_ImportantStrings.Add("VIN 3");
            this.m_ImportantStrings.Add("Vaginal Intraepithelial Neoplasia");
            this.m_ImportantStrings.Add("VAIN III");
            this.m_ImportantStrings.Add("VAIN 3");
            this.m_ImportantStrings.Add("Ain Intraepithelial Neoplasia");
            this.m_ImportantStrings.Add("AIN III");
            this.m_ImportantStrings.Add("AIN 3");
        }
    }
}