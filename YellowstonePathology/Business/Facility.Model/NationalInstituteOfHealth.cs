using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class NationalInstituteOfHealth : Facility
    {
        public NationalInstituteOfHealth()
        {
            this.m_FacilityId = "NIHNCI";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "National Institute Of Health/National Cancer Institute";
            this.m_Address1 = "Bldg. 10/Room 2B50";
            this.m_City = "Bethesda";
            this.m_State = "MD";
            this.m_ZipCode = "20892";
            this.m_IsReferenceLab = true;                        
        }
    }
}
