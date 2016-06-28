using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class MontanaDermatology : Facility
	{        
		public MontanaDermatology()
		{
            this.m_FacilityId = "MTDRMTLGY";
			this.m_FacilityName = "Montana Dermatology and Skin Cancer Center - SV Clinic";
            this.m_IsReferenceLab = false;

			this.m_CliaLicense = new CLIALicense(this, null);
		}
	}
}
