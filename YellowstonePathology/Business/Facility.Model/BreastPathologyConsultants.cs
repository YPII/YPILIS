using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class BreastPathologyConsultants : Facility
    {
        public BreastPathologyConsultants()
        {
            this.m_FacilityId = "BRSTPATHCNLT";
            this.m_FacilityName = "Breast Pathology Consultants";
            this.m_Address1 = "220 21st Avenue South, Suite 250";
            this.m_Address2 = "Suite 250";
            this.m_City = "Nashville";
            this.m_State = "TN";
            this.m_ZipCode = "37212";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
