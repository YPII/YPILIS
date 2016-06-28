using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniPath : Facility
    {
        public UniPath()
        {
            this.m_FacilityId = "UNPTH";            
            this.m_FacilityName = "UniPath";
            this.m_Address1 = "6116 East Warren Avenue";
            this.m_Address2 = null;
            this.m_City = "Denver";
            this.m_State = "CO";
            this.m_ZipCode = "80222";
            this.m_IsReferenceLab = true;					
        }
    }
}
