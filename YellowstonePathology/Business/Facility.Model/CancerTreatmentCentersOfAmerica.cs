using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CancerTreatmentCentersOfAmerica : Facility
    {
        public CancerTreatmentCentersOfAmerica()
        {
            this.m_FacilityId = "CTCOA";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Cancer Treatment Centers Of America";
            this.m_Address1 = "2520 Elisha Ave. ";
            this.m_Address2 = "Attn: PATHOLOGY";
            this.m_City = "Zion";
            this.m_State = "IL";
            this.m_ZipCode = "60099";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
