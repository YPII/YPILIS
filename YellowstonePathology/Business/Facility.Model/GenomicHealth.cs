using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class GenomicHealth : Facility
    {
        public GenomicHealth()
        {
            this.m_FacilityId = "GNMCHLTH";
            this.m_FacilityName = "Genomic Health";
            this.m_City = "Redwood City";
            this.m_State = "CA";
            this.m_IsReferenceLab = true;

			this.m_CliaLicense = new CLIALicense(this, null);
        }
    }
}
