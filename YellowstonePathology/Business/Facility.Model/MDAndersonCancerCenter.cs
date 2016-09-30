using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class MDAndersonCancerCenter : Facility
    {
        public MDAndersonCancerCenter()
        {
            this.m_FacilityId = "MDANDCC";
            this.m_FacilityIdOLD = null;
            this.m_FacilityName = "MD Anderson Cancer Center";
            this.m_Address1 = "1515 holcombe Blvd.";
            this.m_Address2 = "RM G1-3669";
            this.m_City = "Houstin";
            this.m_State = "TX";
            this.m_ZipCode = "77030";
            this.m_IsReferenceLab = true;

            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
