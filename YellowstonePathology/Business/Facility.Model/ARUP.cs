using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ARUP : Facility
    {        
        public ARUP()
        {
            this.m_FacilityId = "ARUPSPD";
            this.m_FacilityIdOLD = "C8B581E6-8D3B-489C-9625-3B6465A556D2";
            this.m_FacilityName = "ARUP Laboratories, Surgical Pathology Division";
            this.m_Address1 = "500 Chipeta Way";
            this.m_City = "Salt Lake City";
            this.m_State = "UT";
            this.m_ZipCode = "84108-1221";
            this.m_PhoneNumber = "(800)522-2787";
            this.m_FedexPaymentType = "SENDER";
            this.m_IsReferenceLab = true;
			
			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
