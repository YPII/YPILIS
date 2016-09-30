using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class YellowstonePathologistBillings : Facility
    {        
        public YellowstonePathologistBillings()
        {
            this.m_FacilityName = "Yellowstone Pathologist Billings";
            this.m_FacilityId = "YPBLGS";
            this.m_FacilityIdOLD = "35C4D327-2623-4AF1-90F9-7F48900A5A96";
            this.m_Address1 = "2900 12th Avenue North";
            this.m_Address2 = "Suite 295W";
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "59101";
            this.m_IsReferenceLab = false;
            this.m_AccessioningLocation = "Billings";
            this.m_LocationAbbreviation = "YPI Blgs, Mt";

            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrBrownOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrDurdenOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrEmerickOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrNeroOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrSchultzOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.SvhPathologistOffice());

            this.m_CliaLicense = new CLIALicense(new YellowstonePathologyInstituteBillings(), "27D0946844");
		}
    }
}
