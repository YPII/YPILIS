using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class StJamesHospital : Facility
    {
        public StJamesHospital()
        {
            this.m_FacilityId = "STJMSHSPTL";            
            this.m_FacilityName = "St. James Hospital";            

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
