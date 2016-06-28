using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class TallmanDermatology : Facility
    {
        public TallmanDermatology()
        {
            this.m_FacilityId = "TLLMNDRM";
            this.m_FacilityName = "Tallman Dermatology";
            this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
