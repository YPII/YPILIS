using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class LocationPerformedComment
    {
        public LocationPerformedComment(string technicalComponentFacilityId, string professionalCom)
        {
            
        }

        /*
        public virtual string GetLocationPerformedComment()
        {
            StringBuilder result = new StringBuilder();
            YellowstonePathology.Business.Facility.Model.Facility technicalComponentFacility = null;
            YellowstonePathology.Business.Facility.Model.Facility professionalComponentFacility = null;

            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection;
            technicalComponentFacility = facilityCollection.GetByFacilityId(this.TechnicalComponentFacilityId);
            professionalComponentFacility = facilityCollection.GetByFacilityId(this.ProfessionalComponentFacilityId);

            if (technicalComponentFacility.CLIALicense.LicenseNumber == professionalComponentFacility.CLIALicense.LicenseNumber)
            {
                if (this.HasTechnicalComponent == true && this.HasProfessionalComponent == false)
                {
                    result.Append("Technical component(s) performed at ");
                    result.Append(technicalComponentFacility.CLIALicense.GetAddressString());
                }
                else if (this.HasProfessionalComponent == true && this.HasTechnicalComponent == false)
                {
                    if (result.Length != 0) result.Append(" ");
                    result.Append("Professional component(s) performed at ");
                    result.Append(professionalComponentFacility.CLIALicense.GetAddressString());
                }
                else if (this.HasProfessionalComponent == true && this.HasTechnicalComponent == true)
                {
                    result.Append("Technical and professional component(s) performed at ");
                    result.Append(professionalComponentFacility.CLIALicense.GetAddressString());
                }
            }
            else
            {
                if (this.HasTechnicalComponent == true)
                {
                    result.Append("Technical component(s) performed at ");
                    result.Append(technicalComponentFacility.CLIALicense.GetAddressString());
                }

                if (this.HasProfessionalComponent == true)
                {
                    if (result.Length != 0) result.Append(" ");
                    result.Append("Professional component(s) performed at ");
                    result.Append(professionalComponentFacility.CLIALicense.GetAddressString());
                }
            }

            return result.ToString();
        }
         */
    }
}
