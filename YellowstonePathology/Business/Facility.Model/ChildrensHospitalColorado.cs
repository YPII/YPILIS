using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ChildrensHospitalColorado : Facility
    {
        public ChildrensHospitalColorado()
        {
            this.m_FacilityId = "CHHOSCO";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "Childrens Hospital Colorado";
            this.m_Address1 = "13123 east 16th Ave.";
            this.m_Address2 = null;
            this.m_City = "Aurora";
            this.m_State = "CO";
            this.m_ZipCode = "80045";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
