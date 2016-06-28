using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class YellowstonePathologistCody : Facility
    {        
        public YellowstonePathologistCody()
        {
            this.m_FacilityId = "YPCDY";          
            this.m_FacilityName = "Yellowstone Pathologist Cody";
            this.m_Address1 = "707 Sheridan Avenue";
            this.m_City = "Cody";
            this.m_State = "WY";
            this.m_ZipCode = "82414";
            this.m_IsReferenceLab = false;
            this.m_AccessioningLocation = "Cody";
            this.m_LocationAbbreviation = "YPI Cody, Wy";

            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice());
            this.m_CliaLicense = new CLIALicense(new YellowstonePathologyInstituteCody(), "53D1091161");
        }
    }
}
