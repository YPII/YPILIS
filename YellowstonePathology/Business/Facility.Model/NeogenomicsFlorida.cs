using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NeogenomicsFlorida : Facility
    {        
        public NeogenomicsFlorida()
        {
            this.m_FacilityId = "NEOGNMCFL";
            this.m_FacilityIdOLD = "CC56EEAA-C223-4591-BD44-7B6C56C80382";
            this.m_FacilityName = "Neogenomics Florida";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
