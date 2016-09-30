using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class BillingsClinicPathology : Location
	{        
        public BillingsClinicPathology()
		{
			this.LocationId = "BLGSCLNCPTH";
			this.m_Description = "Billings Clinic Pathology";            
		}
	}
}
