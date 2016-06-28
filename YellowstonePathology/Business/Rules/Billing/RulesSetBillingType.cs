using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Billing
{
	public class RulesSetBillingType : BaseRules
	{
        private static RulesSetBillingType m_Instance;
        YellowstonePathology.Business.Domain.CptBillingCode m_CptBillingCode;

        private RulesSetBillingType() 
            : base(typeof(YellowstonePathology.Business.Rules.Billing.RulesSetBillingType))
        {
            
        }

        public static RulesSetBillingType Instance
        {
            get
            {                
                if (m_Instance == null)
                {
                    m_Instance = new RulesSetBillingType();
                }
                return m_Instance;
            }
        }

        public YellowstonePathology.Business.Domain.CptBillingCode CptBillingCode
        {
            get { return this.m_CptBillingCode; }
            set { this.m_CptBillingCode = value; }
        }
	}
}
