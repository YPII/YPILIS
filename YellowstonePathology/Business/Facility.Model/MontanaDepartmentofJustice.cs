using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class MontanaDepartmentofJustice : Facility
	{        
		public MontanaDepartmentofJustice()
		{
            this.m_FacilityId = "MTDPTJC";
			this.m_FacilityName = "Montana Department of Justice";
            this.m_IsReferenceLab = false;

			this.m_CliaLicense = new CLIALicense(this, null);
		}
	}
}
