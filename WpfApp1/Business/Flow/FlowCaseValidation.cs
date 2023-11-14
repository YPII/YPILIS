using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace YellowstonePathology.Business.Flow
{
    public class FlowCaseValidation : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);        
        public event PropertyChangedEventHandler PropertyChanged;

        YellowstonePathology.Business.Validation.BrokenRuleCollection m_BrokenRules;

        public FlowCaseValidation()
        {
            this.m_BrokenRules = new YellowstonePathology.Business.Validation.BrokenRuleCollection();
        }

		public bool Validate(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
        {                        
            this.m_BrokenRules.Clear();			
            
			YellowstonePathology.Business.Validation.ValidationMethods.IsBirthdateValid(accessionOrder.PBirthdate, this.m_BrokenRules);
			YellowstonePathology.Business.Validation.ValidationMethods.IsClientIdValid(accessionOrder.ClientId, this.m_BrokenRules);
			YellowstonePathology.Business.Validation.ValidationMethods.IsPhyscianIdValid(accessionOrder.PhysicianId, this.m_BrokenRules);
			YellowstonePathology.Business.Validation.ValidationMethods.IsDistributionValid(panelSetOrder.TestOrderReportDistributionCollection, this.m_BrokenRules);
			YellowstonePathology.Business.Validation.ValidationMethods.IsPatientIdValid(accessionOrder.PatientId, this.m_BrokenRules);            

            this.NotifyPropertyChanged("BrokenRules");
            if (this.m_BrokenRules.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Validation.BrokenRuleCollection BrokenRules
        {
            get { return this.m_BrokenRules; }
        }
    }
}