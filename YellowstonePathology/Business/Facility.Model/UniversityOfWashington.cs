using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class UniversityOfWashington : Facility
    {       
        public UniversityOfWashington()
        {
            this.m_FacilityId = "UWRLS";
            this.m_FacilityName = "University of Washington Medical Center";
            this.m_Address1 = "1959 N.E. Pacific St.";
            this.m_Address2 = "Rm BB 220D";
            this.m_City = "Seattle";
            this.m_State = "WA";
            this.m_ZipCode = "98195";
            this.m_PhoneNumber = "(206)598-6400";
            this.m_IsReferenceLab = true;
            this.m_FedexPaymentType = "SENDER";

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
