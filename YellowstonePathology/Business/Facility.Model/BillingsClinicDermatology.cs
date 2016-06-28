using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
	public class BillingsClinicDermatology : Location
	{
        public BillingsClinicDermatology()
		{
			this.LocationId = "BLGSCLNCDRM";
			this.m_Description = "Billings Clinic Dermatology";            
		}
	}
}
