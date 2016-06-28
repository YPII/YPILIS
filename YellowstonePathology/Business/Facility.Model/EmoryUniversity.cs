using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class EmoryUniversity : Facility
    {
        public EmoryUniversity()
        {
            this.m_FacilityId = "EMRYNVSTY";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Emory University";
            this.m_Address1 = "1364 Clifton Road, N.E.";
            this.m_City = "Atlanta";
            this.m_State = "GA";
            this.m_ZipCode = "30322";
            this.m_IsReferenceLab = true;                        
        }
    }
}
