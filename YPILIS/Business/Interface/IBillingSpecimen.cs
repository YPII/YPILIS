using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IBillingSpecimen
	{
        int BillingSpecimenId { get; set; }
        int BillingAccessionId { get; set; }
        string SpecimenOrderId { get; set; }
        string Description { get; set; }
	}
}
