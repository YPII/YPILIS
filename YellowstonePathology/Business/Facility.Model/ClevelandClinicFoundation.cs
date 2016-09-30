using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ClevelandClinicFoundation : Facility
    {
        public ClevelandClinicFoundation()
        {
            this.m_FacilityId = "CLVLNDCLNC";
            this.m_FacilityName = "Cleveland Clinic Foundation";
            this.m_Address1 = "9500 Euclid Avenue";
            this.m_City = "Cleveland";
            this.m_State = "OH";
            this.m_ZipCode = "44195";
            this.m_IsReferenceLab = true;
	
            this.m_CliaLicense = new CLIALicense(this, string.Empty);            
        }
    }
}
