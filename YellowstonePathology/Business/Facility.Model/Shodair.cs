using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class Showdair : Facility
    {
        public Showdair()
        {
            this.m_FacilityId = "SHDR"; 
            this.m_FacilityName = "Shodair";
            this.m_City = "Helena";
            this.m_State = "MT";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);
		}
    }
}
