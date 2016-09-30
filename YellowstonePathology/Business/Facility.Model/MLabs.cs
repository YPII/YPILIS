using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MLabs : Facility
    {        
        public MLabs()
        {
            this.m_FacilityId = "MLABS";
            this.m_FacilityName = "MLABS";
            this.m_Address1 = "1500 E Medical Center Drive";            
            this.m_City = "Ann Arbor";
            this.m_State = "MI";
            this.m_ZipCode = "48109-5054";
            this.m_IsReferenceLab = true;
			
			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
