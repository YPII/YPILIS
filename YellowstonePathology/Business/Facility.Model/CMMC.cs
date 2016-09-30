using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CMMC : Facility
    {
        public CMMC()
        {
            this.m_FacilityId = "CMMC";
            this.m_FacilityName = "CMMC";            
			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
