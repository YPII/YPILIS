using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelSetOrder
{
	public class RulesPanelSetOrderFinalCommonV2
	{
        protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        protected YellowstonePathology.Business.Rules.Rule m_Rule;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private bool m_Unsigning;

		public RulesPanelSetOrderFinalCommonV2()
        {            
			this.m_Rule = new Rule();						
			this.m_Rule.ActionList.Add(IsAlreadyFinal);
			this.m_Rule.ActionList.Add(IsPatientDataOK);
			this.m_Rule.ActionList.Add(IsLinked);
			this.m_Rule.ActionList.Add(IsClientDataOK);			
			this.m_Rule.ActionList.Add(ResultsAreNotSet);			
			this.m_Rule.ActionList.Add(SetAsFinal);
			this.m_Rule.ActionList.Add(UnSignCase);			
        }				

        public void IsAlreadyFinal()
        {
			this.m_Unsigning = false;
			if (this.m_PanelSetOrder.Final == true) 
			{
				this.m_Unsigning = true;
			}
        }

        public void IsPatientDataOK()
        {
			if (!this.m_Unsigning)
			{
				if (this.m_AccessionOrder.PFirstName == string.Empty) this.m_ExecutionStatus.AddMessage("Patient First Name is empty", true);
				if (this.m_AccessionOrder.PLastName == string.Empty) this.m_ExecutionStatus.AddMessage("Patient Last Name is empty", true);
				if (this.m_AccessionOrder.PBirthdate == null) this.m_ExecutionStatus.AddMessage("Patient Birthdate is empty", true);
			}
        }

        public void IsLinked()
        {
			if (!this.m_Unsigning)
			{

				if (this.m_AccessionOrder.PatientId == "0") this.m_ExecutionStatus.AddMessage("The patient is not linked", true);
			}
        }

        public void IsClientDataOK()
        {
			if (!this.m_Unsigning)
			{
				if (this.m_AccessionOrder.ClientId == 0) this.m_ExecutionStatus.AddMessage("Case Client is not set", true);
				if (this.m_AccessionOrder.PhysicianId == 0) this.m_ExecutionStatus.AddMessage("Case Physician is not set", true);
			}
        }

        public void ResultsAreNotSet()
        {
			if (!this.m_Unsigning)
			{
				if( this.m_PanelSetOrder.ResultsAreSet() == false)
				{
					this.m_ExecutionStatus.AddMessage("One or more case results are not set", true);
				}
			}
        }        
        
		public void DoesSVHCaseHaveAccount()
		{
			if (this.m_AccessionOrder.ClientId == 558)
			{
				if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true)
				{
					this.m_ExecutionStatus.AddMessage("The SVH Account is blank.", true);
				}
			}
		}

		public void DoesSVHCaseHaveMRN()
		{
			if (this.m_AccessionOrder.ClientId == 558)
			{
				if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
				{
					this.m_ExecutionStatus.AddMessage("The SVH MRN is blank.", true);
				}
			}
		}

        public void SetAsFinal()
        {
			if (!this.m_Unsigning)
			{
				this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
			}
        }

		public void UnSignCase()
		{
			if (this.m_Unsigning)
			{
				this.m_PanelSetOrder.Unfinalize();
			}
		}			

        public void Execute(Rules.ExecutionStatus executionStatus,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;			
			this.m_ExecutionStatus = executionStatus;
            this.m_Rule.Execute(executionStatus);
        }
	}
}
