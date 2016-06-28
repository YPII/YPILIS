using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class Therapath : Facility
    {
        public Therapath()
        {
            this.m_FacilityId = "THRPTH"; 
            this.m_FacilityName = "Therapath Neuropathology";
            this.m_Address1 = "545 West 45th Street";
            this.m_City = "New York";
            this.m_State = "NY";
            this.m_ZipCode = "10036";
            this.m_IsReferenceLab = true;
			
			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
