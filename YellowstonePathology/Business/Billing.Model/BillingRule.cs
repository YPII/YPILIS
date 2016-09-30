using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing.Model
{
	public class BillingRule : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        
        private string m_BillingRuleId;
        private string m_BillingRuleSetId;
        private int m_Priority;
        private RuleValue m_PatientType;
        private RuleValue m_PrimaryInsurance;
        private RuleValue m_SecondaryInsurance;
        private RuleValue m_PostDischarge;
        private BillingTypeEnum m_BillingType;

        private PanelSetIdList m_PanelSetIncludeOnlyList;
        private PanelSetIdList m_PanelSetExcludeList;

		public BillingRule()
		{
            this.m_PanelSetIncludeOnlyList = new PanelSetIdList();
            this.m_PanelSetExcludeList = new PanelSetIdList();
		}

        public PanelSetIdList PanelSetIncludeOnlyList
        {
            get { return this.m_PanelSetIncludeOnlyList; }
        }

        public PanelSetIdList PanelSetExcludeList
        {
            get { return this.m_PanelSetExcludeList; }
        }

        public string BillingRuleId
        {
            get { return this.m_BillingRuleId; }
            set
            {
                if (this.m_BillingRuleId != value)
                {
                    this.m_BillingRuleId = value;
                    this.NotifyPropertyChanged("BillingRuleId");
                }
            }
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

        public int Priority
        {
            get { return this.m_Priority; }
            set
            {
                if (this.m_Priority != value)
                {
                    this.m_Priority = value;
                    this.NotifyPropertyChanged("Priority");
                }
            }
        }

        public RuleValue PatientType
        {
            get { return this.m_PatientType; }
            set
            {
                if (this.m_PatientType != value)
                {
                    this.m_PatientType = value;
                    this.NotifyPropertyChanged("PatientType");
                }
            }
        }

        public RuleValue PrimaryInsurance
        {
            get { return this.m_PrimaryInsurance; }
            set
            {
                if (this.m_PrimaryInsurance != value)
                {
                    this.m_PrimaryInsurance = value;
                    this.NotifyPropertyChanged("PrimaryInsurance");
                }
            }
        }

        public RuleValue SecondaryInsurance
        {
            get { return this.m_SecondaryInsurance; }
            set
            {
                if (this.m_SecondaryInsurance != value)
                {
                    this.m_SecondaryInsurance = value;
                    this.NotifyPropertyChanged("SecondaryInsurance");
                }
            }
        }

        public RuleValue PostDischarge
        {
            get { return this.m_PostDischarge; }
            set
            {
                if (this.m_PostDischarge != value)
                {
                    this.m_PostDischarge = value;
                    this.NotifyPropertyChanged("PostDischarge");
                }
            }
        }

        public BillingTypeEnum BillingType
        {
            get { return this.m_BillingType; }
            set
            {
                if (this.m_BillingType != value)
                {
                    this.m_BillingType = value;
                    this.NotifyPropertyChanged("BillingType");
                }
            }
        }            			

        public virtual bool IsMatch(string patientType, string primaryInsurance, string secondaryInsurance, bool postDischarge, int panelSetId)
        {
            bool result = true;
            if (this.m_PatientType.IsMatch(patientType) == false) result = false;
            if (this.m_PrimaryInsurance.IsMatch(primaryInsurance) == false) result = false;
            if (this.m_SecondaryInsurance.IsMatch(secondaryInsurance) == false) result = false;
            if (this.m_PostDischarge.IsMatch(postDischarge) == false) result = false;
            if (this.m_PanelSetExcludeList.Exists(panelSetId) == true) result = false;
            if (this.m_PanelSetIncludeOnlyList.Count != 0 && this.m_PanelSetIncludeOnlyList.Exists(panelSetId) == false) result = false;
            return result;
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
