using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.View
{
	public class ICD9BillingCodeView
	{
		private YellowstonePathology.Business.Billing.Model.ICD9BillingCode m_ICD9BillingCode;

        public ICD9BillingCodeView(YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode)
		{
			this.m_ICD9BillingCode = icd9BillingCode;
		}

		public YellowstonePathology.Business.Billing.Model.ICD9BillingCode ICD9BillingCode
		{
			get { return this.m_ICD9BillingCode; }
		}

		public string ICD9Code
		{
			get { return this.m_ICD9BillingCode.ICD9Code; }
		}

		public int Quantity
		{
			get { return this.m_ICD9BillingCode.Quantity; }
		}
	}
}
