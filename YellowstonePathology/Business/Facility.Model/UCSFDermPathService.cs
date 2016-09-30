using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UCSFDermPathService : Facility
    {        
        public UCSFDermPathService()
        {
            this.m_FacilityId = "UCSFDRM";       
            this.m_FacilityName = "UCSF Derm Path Service";
            this.m_Address1 = "1701 Divisadero St.";
            this.m_Address2 = "Ste 280";
            this.m_City = "San Francisco";
            this.m_State = "CA";
            this.m_ZipCode = "94115-3011";
            this.m_IsReferenceLab = true;

            this.m_FedexPaymentType = "SENDER";
            this.m_CliaLicense = new CLIALicense(this, null);            
        }
    }
}
