using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MayoClinic : Facility
    {        
        public MayoClinic()
        {
            this.m_FacilityId = "MAYO";
            this.m_FacilityName = "MayoClinic";
            this.m_Address1 = "200 First St. SW";            
            this.m_City = "Rochester";
            this.m_State = "MN";
            this.m_ZipCode = "55905";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
