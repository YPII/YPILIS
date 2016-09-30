using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
	public class InsuranceType
	{
		private string m_InsuranceTypeName;

		public InsuranceType()
		{
		}

		public InsuranceType(string insuranceTypeName)
		{
			this.m_InsuranceTypeName = insuranceTypeName;
		}

		public string InsuranceTypeName
		{
			get { return this.m_InsuranceTypeName; }
		}
	}

}
