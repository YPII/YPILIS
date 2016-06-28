using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
	public class InsuranceTypeCollection : ObservableCollection<InsuranceType>
	{
		private readonly List<string> m_InsuranceTypes = new List<string> { "Not Selected", "Medicare", "Medicaid", "BCHP", "BCBS", "Governmental", "Commercial" };

		public InsuranceTypeCollection( bool includeAny)
		{
			if (includeAny == true)
			{
				InsuranceType insuranceType = new InsuranceType("Any");
				this.Add(insuranceType);
			}

			foreach (string name in this.m_InsuranceTypes)
			{
				InsuranceType insuranceType = new InsuranceType(name);
				this.Add(insuranceType);
			}
		}
	}
}
