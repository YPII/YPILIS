using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class BigSkyDermatology : Facility
	{
		public BigSkyDermatology()
		{
			this.m_FacilityId = "BGSKDRMTLGY";
			this.m_FacilityName = "Big Sky Dermatology";
            this.m_IsReferenceLab = false;
			
			this.m_CliaLicense = new CLIALicense(this, string.Empty);
		}
	}
}
