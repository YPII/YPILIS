using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class CockerellDermatology : Facility
    {
        public CockerellDermatology()
        {
            this.m_FacilityId = "CKRLLDRM";
            this.m_FacilityName = "Cockerell Dermatology";
            this.m_Address1 = "2110 Research Row";
            this.m_City = "Dallas";
            this.m_State = "TX";
            this.m_ZipCode = "75235";
            this.m_IsReferenceLab = true;            
        }
    }
}
