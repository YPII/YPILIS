using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing.Model
{
	public class BillingRuleSet : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        protected string m_BillingRuleSetId;
        protected string m_BillingRuleSetIdOld;
        protected string m_BillingRuleSetName;

        protected BillingRuleCollection m_BillingRuleCollection;

		public BillingRuleSet()
		{
            this.m_BillingRuleCollection = new BillingRuleCollection();
		}

        public BillingRuleCollection BillingRuleCollection
        {
            get { return this.m_BillingRuleCollection; }
        }

        public string BillingRuleSetId
        {
            get { return this.m_BillingRuleSetId; }
            set
            {
                if (this.m_BillingRuleSetId != value)
                {
                    this.m_BillingRuleSetId = value;
                    this.NotifyPropertyChanged("BillingRuleSetId");
                }
            }
        }

        public string BillingRuleSetIdOld
        {
            get { return this.m_BillingRuleSetIdOld; }
            set
            {
                if (this.m_BillingRuleSetIdOld != value)
                {
                    this.m_BillingRuleSetIdOld = value;
                    this.NotifyPropertyChanged("BillingRuleSetIdOld");
                }
            }
        }

        public string BillingRuleSetName
        {
            get { return this.m_BillingRuleSetName; }
            set
            {
                if (this.m_BillingRuleSetName != value)
                {
                    this.m_BillingRuleSetName = value;
                    this.NotifyPropertyChanged("BillingRuleSetName");
                }
            }
        }

        public YellowstonePathology.Business.Billing.Model.BillingTypeEnum GetBillingType(string patientType, string primaryInsurance, string secondaryInsurance, bool postDischarge, int panelSetId)
        {            
            BillingRule matchedBillingRule = this.m_BillingRuleCollection.GetMatch(patientType, primaryInsurance, secondaryInsurance, postDischarge, panelSetId);
            return matchedBillingRule.BillingType;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
