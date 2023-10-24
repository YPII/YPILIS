using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
	public class BillingRuleMatch
	{
		private string m_FieldName;
		string m_BillableValue;
		string m_RuleValue;
		bool m_Matches;

		public BillingRuleMatch(string fieldName, string billableValue)
		{
			this.m_FieldName = fieldName;
			this.m_BillableValue = billableValue;
		}

		public void SetMatchValues(string ruleValue)
		{
			this.m_RuleValue = ruleValue;
		}

		public void MatchValues()
		{
			this.m_Matches = false;

			if (this.m_RuleValue == "Any")
			{
				this.m_Matches = true;
			}
			else if (string.IsNullOrEmpty(this.m_BillableValue) == true)
			{
				if (this.m_RuleValue == "Any")
				{
					this.m_Matches = true;
				}
			}
			else if (this.m_BillableValue.ToUpper() == this.m_RuleValue.ToUpper())
			{
				this.m_Matches = true;
			}
		}

		public string FieldName
		{
			get { return this.m_FieldName; }
		}

		public bool Matches
		{
			get { return this.m_Matches; }
		}
	}
}
