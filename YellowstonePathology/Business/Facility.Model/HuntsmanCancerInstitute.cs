using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class HuntsmanCancerInstitute : Facility
    {
        public HuntsmanCancerInstitute()
        {
            this.m_FacilityId = "HNTMNCI";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Huntsman Cancer Intitute";
            this.m_Address1 = "1950 Circle of Hope";
            this.m_Address2 = "Suite 3100";
            this.m_City = "Salt Lake City";
            this.m_State = "UT";
            this.m_ZipCode = "84112-5550";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
