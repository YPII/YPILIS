using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class FoundationOne : Facility
    {
        public FoundationOne()
        {
            this.m_FacilityId = "FNDONE";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Foundation One";
            this.m_Address1 = "7010 Kit Creek Rd";
            this.m_City = "Morrisville";
            this.m_State = "NC";
            this.m_ZipCode = "9999999999";
            this.m_IsReferenceLab = true;                        
        }
    }
}
