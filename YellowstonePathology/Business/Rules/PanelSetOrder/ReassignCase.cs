using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelSetOrder
{
	public class ReassignCase
	{
		private YellowstonePathology.Business.Rules.Rule m_Rule;
        private YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private bool m_CreateAmendment;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public ReassignCase()
		{
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

			this.m_Rule.ActionList.Add(this.UserIsPathologist);
			this.m_Rule.ActionList.Add(this.CreateAmendment);
			this.m_Rule.ActionList.Add(this.ChangeUser);
		}

        public void Execute(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, 
            bool createAmendment, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_ExecutionStatus = executionStatus;
			this.m_PanelSetOrderItem = panelSetOrder;
			this.m_CreateAmendment = createAmendment;
            this.m_SystemIdentity = systemIdentity;
			this.m_Rule.Execute(executionStatus);
		}

		private void UserIsPathologist()
		{			
			if (!this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist))
			{
				this.m_ExecutionStatus.AddMessage("You must be a pathologist to reassign a case.", true);
				this.m_ExecutionStatus.Halted = true;
			}
		}

		private void CreateAmendment()
		{
			if (this.m_CreateAmendment)
			{
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_PanelSetOrderItem.AddAmendment();
				amendment.UserId = this.m_SystemIdentity.User.UserId;				
			}
		}

		private void ChangeUser()
		{			
			this.m_PanelSetOrderItem.AssignedToId = this.m_SystemIdentity.User.UserId;
		}
	}
}
