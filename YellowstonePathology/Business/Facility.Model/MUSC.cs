using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MUSC : Facility
    {
        public MUSC()
        {
            this.m_FacilityId = "MUSC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Medical University of South Carolina";
            this.m_Address1 = "165 Ashley Ave Suite 309";
            this.m_City = "Charleston";
            this.m_State = "SC";
            this.m_ZipCode = "29425-9080";
            this.m_IsReferenceLab = true;                        
        }
    }
}
