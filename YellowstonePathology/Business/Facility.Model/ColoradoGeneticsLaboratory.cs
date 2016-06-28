using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ColoradoGeneticsLaboratory : Facility
    {
        public ColoradoGeneticsLaboratory()
        {
            this.m_FacilityId = "COGNTCLBTRY";
            this.m_FacilityName = "Colorado Genetics Laboratory";
            this.m_Address1 = "3055 Roslyn Street";
            this.m_Address2 = "Suite 200";
            this.m_City = "Denver";
            this.m_State = "CO";
            this.m_ZipCode = "80238";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, string.Empty);
        }
    }
}
