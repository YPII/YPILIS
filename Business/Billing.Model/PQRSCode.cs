using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCode : CptCode
    {
        protected string m_ReportingDefinition;
		protected string m_Header;

        public PQRSCode()
        {

        }

        [PersistentProperty()]
        public string ReportingDefinition
        {
            get { return this.m_ReportingDefinition; }
            set { this.m_ReportingDefinition = value; }
        }

        [PersistentProperty()]
        public string Header
		{
			get { return this.m_Header; }
            set { this.m_Header = value; }
		}

		public string FormattedReportingDefinition
		{
			get
			{
				StringBuilder result = new StringBuilder(this.m_Code);
				if (this.m_Modifier != null) result.Append("-" + this.m_Modifier);
				result.Append(":  ");
				result.Append(this.m_ReportingDefinition);
				return result.ToString();
			}
		}

        public override string GetModifier(BillingComponentEnum billingComponent)
        {
            return this.m_Modifier;
        }
    }
}
