using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class LoweLawGroup : Facility
    {
        public LoweLawGroup()
        {
            this.m_FacilityId = "LWLWGRP";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Lowe Law Group";
            this.m_Address1 = "6028 S. Ridgeline Dr., Ste 203";
            this.m_City = "Ogden";
            this.m_State = "UT";
            this.m_ZipCode = "84405";
            this.m_IsReferenceLab = false;                        
        }
    }
}
