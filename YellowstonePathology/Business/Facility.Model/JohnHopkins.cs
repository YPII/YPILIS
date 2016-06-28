using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class JohnHopkins : Facility
    {
        public JohnHopkins()
        {
            this.m_FacilityId = "JHNHLKNS";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "John Hopkins Reference Laboratories";
            this.m_Address1 = "401 N. Broadway";
            this.m_City = "Baltimore";
            this.m_State = "MD";
            this.m_ZipCode = "21231-240";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
