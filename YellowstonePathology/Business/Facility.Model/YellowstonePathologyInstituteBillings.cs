using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class YellowstonePathologyInstituteBillings : Facility
    {        
        public YellowstonePathologyInstituteBillings()
        {
            this.m_FacilityId = "YPIBLGS";
            this.m_FacilityName = "Yellowstone Pathology Institute Billings";            
            this.m_FacilityIdOLD = "98843B5A-BA73-4512-B4A7-F7E2E292D754";
            this.m_Address1 = "2900 12th Avenue North";
            this.m_Address2 = "Suite 295W";
            this.m_City = "Billings";
            this.m_State = "MT";
            this.m_ZipCode = "59101";
            this.m_PhoneNumber = "(406)860-3984";
            this.m_IsReferenceLab = false;
            this.m_AccessioningLocation = "Billings";
            this.m_LocationAbbreviation = "YPI Blgs, Mt";

            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCaseCompilationStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCuttingStationCaptain());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCuttingStationTenille());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCytologyLoginStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsFlowAStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsFlowBStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsFlowCStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsGrossHobbitStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsGrossPathStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsGrossTechStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsHistologyAStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsHistologyBStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsHistologyIHCStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsICStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsIT3Station());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsMolecularAStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsMolecularBStation());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.TheDarkRoom());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DonaCranstonOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.EricRamseyOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.JoiGarzaOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.JulieBlaschakOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.KevinBengeOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.MelissaMelbyOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.MikeBoydOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.ChelsyOrtloffOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.RobertaBeckersOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.SidHarderOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.TiffanyGoodsonOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.WilliamCoplandOffice());
            this.Locations.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.YolandaHuttonOffice());

            this.m_CliaLicense = new CLIALicense(this, "27D0946844");
		}
    }
}
