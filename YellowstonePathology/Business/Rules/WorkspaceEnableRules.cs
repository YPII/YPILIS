using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Rules
{
	public class WorkspaceEnableRules
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Common.FieldEnabler m_FieldEnabler;        
		YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

		public WorkspaceEnableRules()
		{
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
			this.m_Rule.ActionList.Add(this.LockAcquired);
			this.m_Rule.ActionList.Add(this.IsFinal);
			this.m_Rule.ActionList.Add(this.UserIsPathologist);
			this.m_Rule.ActionList.Add(this.HasOpenAmendment);
			this.m_Rule.ActionList.Add(this.UserIsTyping);
			this.m_Rule.ActionList.Add(this.BillingAudited);
		}

		public void LockAcquired()
		{
			if (this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
			{
				this.m_FieldEnabler.IsProtectedEnabled = true;
				this.m_FieldEnabler.IsUnprotectedEnabled = true;
				this.m_FieldEnabler.IsSignatureButtonEnabled = true;
			}
			else
			{
				this.m_FieldEnabler.IsProtectedEnabled = false;
				this.m_FieldEnabler.IsUnprotectedEnabled = false;
				this.m_FieldEnabler.IsSignatureButtonEnabled = false;
				this.m_FieldEnabler.IsBillingAuditEnabled = false;
				this.m_ExecutionStatus.Halted = true;
			}
		}

		public void IsFinal()
		{
			if(this.m_PanelSetOrder.Final)
			{
				this.m_FieldEnabler.IsProtectedEnabled = false;
				this.m_FieldEnabler.IsUnprotectedEnabled = true;
				this.m_FieldEnabler.IsSignatureButtonEnabled = false;
			}
			else
			{
				this.m_FieldEnabler.IsProtectedEnabled = true;
				this.m_FieldEnabler.IsUnprotectedEnabled = true;
				this.m_FieldEnabler.IsSignatureButtonEnabled = true;
			}
		}

		public void UserIsPathologist()
		{			
			if (YellowstonePathology.Business.User.SystemIdentity.Instance.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist))
			{
				this.m_FieldEnabler.IsUnprotectedEnabled = true;
			}
		}

		public void HasOpenAmendment()
		{
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in m_PanelSetOrder.AmendmentCollection)
			{
				if (amendment.Final == false)
				{
					this.m_FieldEnabler.IsProtectedEnabled = true;
					this.m_FieldEnabler.IsUnprotectedEnabled = true;
					this.m_FieldEnabler.IsSignatureButtonEnabled = false;
					break;
				}
			}
		}

		public void UserIsTyping()
		{			            
			if (YellowstonePathology.Business.User.SystemIdentity.Instance.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.SurgicalCaseTyping))
			{
				this.m_FieldEnabler.IsUnprotectedEnabled = true;
			}
		}

		public void BillingAudited()
		{
			if (this.m_PanelSetOrder.Audited == true)
			{
				this.m_FieldEnabler.IsBillingAuditEnabled = false;
			}
			else
			{
				this.m_FieldEnabler.IsBillingAuditEnabled = true;
			}
		}

		public void Execute(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
			YellowstonePathology.Business.Common.FieldEnabler fieldEnabler, 
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;
			this.m_FieldEnabler = fieldEnabler;
			this.m_ExecutionStatus = executionStatus;            
			this.m_Rule.Execute(this.m_ExecutionStatus);
		}
	}
}
