using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class PhenoPath : Facility
    {
        public PhenoPath()
        {
            this.m_FacilityId = "PHNPTH";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "PhenoPath";
            this.m_Address1 = "551 North 34th Street";
            this.m_Address2 = "Suite 100";
            this.m_City = "Seattle";
            this.m_State = "WA";
            this.m_ZipCode = "98103-8675";
            this.m_IsReferenceLab = true;                        
        }
    }
}
