using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IBillingSpecimenCollection
	{
        Collection<YellowstonePathology.Business.Interface.IBillingSpecimen> BillingSpecimenCollection { get; set; }
	}
}
