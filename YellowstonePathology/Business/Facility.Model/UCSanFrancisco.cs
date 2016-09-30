using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UCSanFrancisco : Facility
    {       
        public UCSanFrancisco()
        {
            this.m_FacilityId = "UCSANFRAN";     
            this.m_FacilityName = "UC San Francisco";
            this.m_Address1 = "505 Pamassus Ave.";
            this.m_Address2 = "M563";
            this.m_City = "San Francisco";
            this.m_State = "CA";
            this.m_ZipCode = "94143";
            this.m_IsReferenceLab = true;

            this.m_FedexPaymentType = "SENDER";
            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
