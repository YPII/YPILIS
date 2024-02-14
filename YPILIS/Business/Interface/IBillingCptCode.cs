using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IBillingCptCode
	{
        int BillingCptCodeId { get; set; }
        int BillingSpecimenId { get; set; }
        string CptCode { get; set; }
        //int Quantity { get; set; }
        Nullable<DateTime> BillingDate { get; set; }
        //string BillingType { get; set; }
        //string FeeType { get; set; }
        //bool Ordered14DaysPostDischarge { get; set; }
	}
}
