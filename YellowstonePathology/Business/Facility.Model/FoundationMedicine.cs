using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class FoundationMedicine : Facility
    {
        public FoundationMedicine()
        {
            this.m_FacilityId = "FNDMDCN";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Foundation Medicine";
            this.m_Address1 = "One Kendall Sq, B3501";
            this.m_City = "Cambridge";
            this.m_State = "MA";
            this.m_ZipCode = "02139";
            this.m_IsReferenceLab = true;                        
        }
    }
}
