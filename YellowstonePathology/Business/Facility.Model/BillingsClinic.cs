using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class BillingsClinic : Facility
    {
        public BillingsClinic()
        {
            this.m_FacilityId = "BLGSCLNC";
            this.m_FacilityName = "Billings Clinic";
            this.m_Address1 = "2800 10th Avenue North";
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "59101";
            this.m_IsReferenceLab = true;
	
            this.m_CliaLicense = new CLIALicense(this, string.Empty);

            this.m_Locations.Add(new BillingsClinicDermatology());
            this.m_Locations.Add(new BillingsClinicPathology());
        }
    }
}
