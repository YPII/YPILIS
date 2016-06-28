using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UAMS : Facility
    {
        public UAMS()
        {
            this.m_FacilityId = "UAMS Department of Anatomic & Clinical Pathology";
            this.m_FacilityName = "UAMS";
            this.m_Address1 = "4301 West markham Street";            
            this.m_City = "Little Rock";
            this.m_State = "AR";
            this.m_ZipCode = "72205";
            this.m_IsReferenceLab = true;
			
			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
