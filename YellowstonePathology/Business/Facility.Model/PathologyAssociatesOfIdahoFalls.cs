using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class PathologyAssociatesOfIdahoFalls : Facility
    {
        public PathologyAssociatesOfIdahoFalls()
        {
            this.m_FacilityId = "PAOIF";
            this.m_FacilityName = "PathologyAssociatesOfIdahoFalls";
            this.m_Address1 = "3100 Channing Way";
            this.m_City = "Idaho Falls";
            this.m_State = "ID";
            this.m_ZipCode = "83404";
            this.m_IsReferenceLab = true;
	
            this.m_CliaLicense = new CLIALicense(this, string.Empty);            
        }
    }
}
