using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
	public class PhysicianConfirmation
	{
		public static bool IsNPIValid(string npi)
		{
			bool result = true;
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(npi);
			if (physician == null)
			{
				result = false;
			}
			return result;
		}
	}
}
