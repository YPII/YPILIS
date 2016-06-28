using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfMichigan : Facility
    {
        public UniversityOfMichigan()
        {
            this.m_FacilityId = "UOMHS";
            this.m_FacilityName = "University Of Michigan Health System"; 
            this.m_Address1 = "1500 East Medical Center Drive";            
            this.m_City = "Ann Arbor";
            this.m_State = "MI";
            this.m_ZipCode = "48109-5054";
            this.m_IsReferenceLab = true;
			
			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
