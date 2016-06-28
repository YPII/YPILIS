using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class YellowstonePathologyInstituteCody : Facility
    {        
        public YellowstonePathologyInstituteCody()
        {
            this.m_FacilityId = "YPICDY";
            this.m_FacilityName = "Yellowstone Pathology Institute Cody";             
            this.m_Address1 = "707 Sheridan Avenue";
            this.m_City = "Cody";
            this.m_State = "WY";
            this.m_ZipCode = "82414";
            this.m_IsReferenceLab = false;
            this.m_AccessioningLocation = "Cody";
            this.m_LocationAbbreviation = "YPI Cody, Wy";

            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.CodyAccessionStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.CodyGrossStation());

            this.m_CliaLicense = new CLIALicense(this, "53D1091161");
        }
    }
}
