using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class TXChildrensHospital : Facility
    {
        public TXChildrensHospital()
        {
            this.m_FacilityId = "TXSCHLDRNSHSPTL";
            this.m_FacilityName = "Texas Children's Hospital";
            this.m_Address1 = "6621 Fannin St. MC 1-2261";
            this.m_Address2 = "A.B180.03";
            this.m_City = "Houston";
            this.m_State = "TX";
            this.m_ZipCode = "77030";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
