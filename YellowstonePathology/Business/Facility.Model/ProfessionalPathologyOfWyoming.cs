using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ProfessionalPathologyOfWyoming : Facility
    {
        public ProfessionalPathologyOfWyoming()
        {
            this.m_FacilityId = "PPWY";
            this.m_FacilityName = "Professional Pathology Of Wyoming";
            this.m_Address1 = "111 S. Jefferson  Ste. #150";
            this.m_City = "Casper";
            this.m_State = "WY";
            this.m_ZipCode = "82601";
            this.m_IsReferenceLab = true;
	
            this.m_CliaLicense = new CLIALicense(this, string.Empty);            
        }
    }
}
